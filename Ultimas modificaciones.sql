--Listar Boleta
alter procedure [dbo].[sp_lis_boleta] @num_doc varchar(15),@mes int,@ano int
as
begin
declare @idPersonal int
declare @dom_asistencia int
declare @asistencia int
declare @tardanza int
declare @feriado_asistencia int
declare @dias_permisos int
declare @dias_vacaciones int
declare @monto_perm decimal

set @asistencia=[dbo].[fDias_Trabajados](@num_doc,@mes,@ano)

set @dom_asistencia= (select count(diaAsistencia) from Asistencia inner join Personal on Personal.IdPersonal= Asistencia.IdPersonal
where month(diaAsistencia)=@mes and year(diaAsistencia)=@ano and datepart(dw,diaAsistencia)= 7 and Personal.numeroDocumentoPersonal = @num_doc)

set @tardanza= (select sum(case when tardanzaAsistencia = 1 then 1 else 0 end) tardanza from Asistencia a inner join Personal p on a.IdPersonal=p.IdPersonal
	where p.numeroDocumentoPersonal=@num_doc and MONTH(a.diaAsistencia)=@mes and  YEAR(a.diaAsistencia)=@ano)
	if (@tardanza is null)
	begin
	set @tardanza=0;
	end
set @feriado_asistencia= dbo.f_feriados_trabajados(@num_doc, @mes, @ano)
set @idPersonal=(select IdPersonal from Personal where numeroDocumentoPersonal=@num_doc)
set @dias_permisos=(select dias_p from [dbo].[fPermiso_mes](@idPersonal,@mes,@ano))
set @monto_perm=(select monto_p from [dbo].[fPermiso_mes](@idPersonal,@mes,@ano))
set @dias_vacaciones=[dbo].[fDiasVacaciones_mes](@idPersonal,@mes,@ano)

select Personal.IdPersonal id_personal, apellidoPaternoPersonal, apellidoMaternoPersonal, nombrePersonal, nombreCargo, montoSalarioCargo ,planillaPersonal, 
@asistencia as asistencia, @dom_asistencia as domingos, @tardanza as tardanza, @feriado_asistencia as feriados,
@dias_permisos as dias_permiso,@monto_perm as monto_permiso,@dias_vacaciones as dias_vacaciones
from  Personal inner join Cargo on Cargo.IdCargo = Personal.IdCargo 
where numeroDocumentoPersonal = @num_doc 
end
go


-- Listar Descuentos Planilla
create procedure sp_lis_descuento_planilla
as
select * from descuentoPlanilla where nombreDescuentoPlanilla ='AFP'
go
-- Consulta boleta para validacion
create procedure sp_consultar_boleta
@numeroBoleta char(10)
as
begin
select numeroBoleta from Boleta where numeroBoleta=@numeroBoleta
end
GO
-- Listar Remuneraciones
ALTER procedure [dbo].[sp_lis_boleta_remuneracion] @num_doc varchar(15)
as
begin
select nombreRemuneracion, montoRemuneracion from Personal_Remuneracion inner join Personal on Personal_Remuneracion.IdPersonal = Personal.IdPersonal
inner join Remuneracion on Personal_Remuneracion.IdRemuneracion = Remuneracion.IdRemuneracion
where numeroDocumentoPersonal = @num_doc
end
go

-- Listar Movimientos
create procedure sp_lis_movimientos_personal
@num_Doc varchar(15),
@mes int,
@ano int
as
begin
declare @idPersonal int
set @idPersonal=(select IdPersonal from Personal where numeroDocumentoPersonal=@num_Doc)
select sum(Movimientos.montoMovimiento) 
as monto,TipoMovimiento.IdTipoMovimiento as idTipoMovimiento,TipoMovimiento.nombreTipoMovimiento as nombreTipoMovimiento  
from Movimientos inner join TipoMovimiento 
on Movimientos.IdTipoMovimiento=TipoMovimiento.IdTipoMovimiento
inner join Personal on Personal.IdPersonal=Movimientos.IdPersonal
where Movimientos.IdPersonal=@idPersonal and MONTH(Movimientos.fechaMovimiento)=@mes and year(Movimientos.fechaMovimiento)=@ano
group by TipoMovimiento.IdTipoMovimiento,TipoMovimiento.nombreTipoMovimiento 
end
go


--------listar descuento
create procedure [dbo].[sp_lis_boleta_descuento] @num_doc varchar(15)
as
begin
select nombreDescuento, sum(montoDescuento) as 'montoDescuento' from Personal_Descuento inner join Personal on Personal_Descuento.IdPersonal = Personal.IdPersonal
inner join Descuento on Personal_Descuento.IdDescuento = Descuento.IdDescuento
where numeroDocumentoPersonal = @num_doc group by nombreDescuento
end
go 



--Insertar Boleta
create procedure [dbo].[sp_ins_boleta_insertar] @numeroBoleta char(10), 
@fechaEmisionBoleta date, @netoAcobrarBoleta decimal(10,2), @idPersonal int
as
insert into Boleta(numeroBoleta, fechaEmisionBoleta,netoAcobrarBoleta,idPersonal)
values(@numeroBoleta, @fechaEmisionBoleta, @netoAcobrarBoleta, @idPersonal)
go
--exec [sp_ins_boleta_insertar] '100020167','2017-07-22',123456, 1000

--Insertar Boleta Detalle
create procedure [dbo].[sp_ins_boleta_detalle_insertar] @idPersonal int, @numeroBoleta char(10), @mes int, @ano int
as
declare @idMovimiento int
DECLARE cursor_movimiento CURSOR STATIC
FOR select IdMovimiento from Movimientos 
where year(fechaMovimiento)=@ano and month(fechaMovimiento)= @mes and IdPersonal = @idPersonal
OPEN cursor_movimiento;
FETCH cursor_movimiento INTO @idMovimiento;
WHILE (@@FETCH_STATUS = 0 )
BEGIN;
	insert into BoletaDetalle(numeroBoleta, IdMovimiento) values(@numeroBoleta,@idMovimiento)
	FETCH cursor_movimiento INTO @idMovimiento;
END;
CLOSE cursor_movimiento;
DEALLOCATE cursor_movimiento;	
go
--exec [sp_ins_boleta_detalle_insertar] 1000, '100020167', 7,2016

--Insertar Boleta Planilla
create procedure [dbo].[sp_insertar_boleta_planilla_insertar] @numeroBoleta char(10), @idDescuentoPlanilla int,
@montoBoletaPlanilla decimal(10,3)
as
insert into Boleta_Planilla(IdDescuentoPlanilla,numeroBoleta,montoBoletaPlanilla)
values(@idDescuentoPlanilla,@numeroBoleta,@montoBoletaPlanilla)
go 
--exec [sp_insertar_boleta_planilla_insertar] '100020167', 1, 100


--dias trabajados
create function fDias_Trabajados(@num_doc varchar(15),@mes int,@ano int)
returns int
as
BEGIN
DECLARE @asistencia int 
--@total int,
--@tardanza int,
--@i int,
--@count int;
	--set @i=1
	--set @count=0
	begin
	set @asistencia= (select COUNT(a.IdAsistencia) dias_trabajados from Asistencia a inner join Personal p on a.IdPersonal=p.IdPersonal
	where p.numeroDocumentoPersonal=@num_doc and MONTH(a.diaAsistencia)=@mes and  YEAR(a.diaAsistencia)=@ano)
	end
	/*begin
	set @tardanza= (select sum(case when tardanzaAsistencia = 1 then 1 else 0 end) tardanza from Asistencia a inner join Personal p on a.IdPersonal=p.IdPersonal
	where p.numeroDocumentoPersonal=@num_doc and MONTH(a.diaAsistencia)=@mes and  YEAR(a.diaAsistencia)=@ano)
	end
	begin
	while(@i<=@tardanza)
	begin
		if(@i%3=0)
		begin 
		set @count=@count+1
		end
		set @i=@i+1
	end
	end*/
	--set @total=@asistencia
	--return @total-@count
	return @asistencia
END
go
--Calcular feriados trabajados
create function [dbo].[f_feriados_trabajados](@num_doc int,@mes int,@ano int)
returns int
as
BEGIN
DECLARE @D date,@fer_a int,@aux_a int;
set @fer_a=0;
set @aux_a=0;
	begin
		DECLARE cursor_asis CURSOR STATIC
		FOR select fechaFeriado from Feriados where MONTH(fechaFeriado)=@mes
		OPEN cursor_asis;
		FETCH cursor_asis INTO @D;
		WHILE (@@FETCH_STATUS = 0 )
		BEGIN;
			set @aux_a=(select count(diaAsistencia) from Asistencia a inner join Personal p on a.IdPersonal=p.IdPersonal
						where p.numeroDocumentoPersonal=@num_doc and MONTH(diaAsistencia)=@mes and YEAR(diaAsistencia)=@ano and diaAsistencia=@D)
			set @fer_a=@fer_a+@aux_a
			FETCH cursor_asis INTO @D;
		END;
		CLOSE cursor_asis;
		DEALLOCATE cursor_asis;
	end
	return @fer_a
END
-------------------------------------------------------------------------------------
--PERMISOS
-------------------------------------------------------------------------------------
go

create function fPermiso_mes (@id_personal int,@mes int,@ano int)
returns  @T table(dias_p int,monto_p decimal) 
as
BEGIN
DECLARE @f1 date, @f2 date,@dias_p int,@auxi int,@monto decimal,@monto_acum decimal;
set @dias_p=0;
set @auxi=0;
set @monto_acum=0;
	begin
		DECLARE cursor_per CURSOR STATIC
		FOR select fechaInicioPermiso,fechaFinPermiso,tp.remuneracionTipoPermiso from Permiso p inner join TipoPermiso tp on p.idTipoPermiso=tp.idTipoPermiso
		where IdPersonal=@id_personal and tp.remuneracionTipoPermiso!=0.0 
		and ((MONTH(fechaInicioPermiso)=@mes and YEAR(fechaInicioPermiso)=@ano) or (MONTH(fechaFinPermiso)=@mes and YEAR(fechaFinPermiso)=@ano))
		OPEN cursor_per;
		FETCH cursor_per INTO @f1, @f2,@monto;
		WHILE (@@FETCH_STATUS = 0 )
		BEGIN;
			set @auxi=[dbo].[fObtener_dias_permiso_vacaciones](@f1,@f2,@mes)
			set @dias_p=@dias_p+@auxi
			set @monto_acum= @monto_acum+ (@auxi*@monto)
			FETCH cursor_per INTO @f1, @f2, @monto;
		END;
		CLOSE cursor_per;
		DEALLOCATE cursor_per;
	end
	insert @T select @dias_p,@monto_acum
	return
END

-------------------------------------------------------------------------------------------------
--VACACIONES
-----------------------------------------------------------------------------------------------------

go
create function fDiasVacaciones_mes (@id_personal int,@mes int,@ano int)
returns  int
as
BEGIN
DECLARE @f1 date, @f2 date,@dias_v int,@auxi int;
set @dias_v=0;
set @auxi=0;
	begin
		DECLARE cursor_vac CURSOR STATIC
		FOR select fechaInicioVacaciones,fechaFinVacaciones from Vacaciones
		where IdPersonal=@id_personal 
		and ((MONTH(fechaInicioVacaciones)=@mes and YEAR(fechaInicioVacaciones)=@ano) or (MONTH(fechaFinVacaciones)=@mes and YEAR(fechaFinVacaciones)=@ano))
		OPEN cursor_vac;
		FETCH cursor_vac INTO @f1, @f2;
		WHILE (@@FETCH_STATUS = 0 )
		BEGIN;
			set @auxi=[dbo].[fObtener_dias_permiso_vacaciones](@f1,@f2,@mes)
			set @dias_v=@dias_v+@auxi
			FETCH cursor_vac INTO @f1, @f2;
		END;
		CLOSE cursor_vac;
		DEALLOCATE cursor_vac;
	end
	
	return @dias_v
END
go
-------------------------------------------------------------------------------------------------------

create function [dbo].[fObtener_dias_permiso_vacaciones](@fecha_inicio date,@fecha_fin date,@mes int)
returns int
as
begin
declare @aux date
declare @i int
set @aux=@fecha_inicio
set @i=0
while(@aux<=@fecha_fin)
begin
	begin
	if(MONTH(@fecha_inicio)=@mes and MONTH(@fecha_fin)=@mes)
		set @i=@i+1
	
	if(MONTH(@fecha_inicio)<@mes and MONTH(@fecha_fin)=@mes)
		if(MONTH(@aux)=MONTH(@fecha_fin))
			set @i=@i+1

	if (MONTH(@fecha_inicio)=@mes and MONTH(@fecha_fin)>@mes)
		if(MONTH(@fecha_inicio)=MONTH(@aux))
		set @i=@i+1
	if (MONTH(@fecha_inicio)<@mes and MONTH(@fecha_fin)>@mes)
		if(@mes=MONTH(@aux))
		set @i=@i+1
	end
	begin
	set @aux=DATEADD(day,1,@aux)
	end
end
return @i
end

GO