$(document).on("ready", function () {
    $("#provincia").prop("disabled", true);
    $(document).on("change", "#list_departamento", function (e) {
        e.preventDefault();
        $("#provincia").prop("disabled", false);
        var parametros = new Object();
        parametros.departamento_id = $("#list_departamento").val();
        $.ajax({
            type: "POST",
            contentType:"application/json; charset=utf-8",
            url: "WfEmpleados.aspx/listarProvincia",
            data: JSON.stringify(parametros),
            dataType: "json",
            async: true,
            success: function (data) {
                var todo = "<option value='0'> --Seleccione la provincia-- </option>";
                $.each(data.d, function (key, value) {
                    var parte = "<option value=" + value['IdProvincia'] + "> " + value['nombreProvincia'] + " </option>";
                    todo += parte;
                });
                $("#provincia").html(todo);
            },
            error: function (data) {
                console.info('error');
            }
        });
    });

    $("#filtrar_distrito").prop("disabled", true);
    $(document).on("change", "#provincia", function (e) {
        var valor = $(this).val();
        e.preventDefault();
        if (valor != "0") {
            $("#filtrar_distrito").prop("disabled", false);
            //listarDistrito
            var parametros = new Object();
            parametros.provincia_id = $("#provincia").val();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "WfEmpleados.aspx/listarDistrito",
                data: JSON.stringify(parametros),
                dataType: "json",
                async: true,
                success: function (data) {
                    //console.log(data);
                    $("#filtrar_distrito").typeahead('destroy');
                    list_filtro = data.d;
                    var arreglo = Array();
                    $.each($(data.d), function (a, b) {
                        arreglo[a] = b.nombreDistrito;
                    });
                    $("#filtrar_distrito").typeahead({ source: arreglo });
                },
                error: function (data) {
                    console.info('error');
                }
            });
        }
    });
    //ubicacion para la empresa
    $("#provincia_empresa").prop("disabled", true);
    $(document).on("change", "#list_departamento_empresa", function (e) {
        e.preventDefault();
        $("#provincia_empresa").prop("disabled", false);
        var parametros = new Object();
        parametros.departamento_id = $("#list_departamento_empresa").val();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "WfEmpleados.aspx/listarProvincia",
            data: JSON.stringify(parametros),
            dataType: "json",
            async: true,
            success: function (data) {
                var todo = "<option value='0'> --Seleccione la provincia-- </option>";
                $.each(data.d, function (key, value) {
                    var parte = "<option value=" + value['provincia_id'] + "> " + value['provincia_nombre'] + " </option>";
                    todo += parte;
                });
                $("#provincia_empresa").html(todo);
            },
            error: function (data) {
                console.info('error');
            }
        });
    });
    $("#distrito_empresa").prop("disabled", true);
    $(document).on("change", "#provincia_empresa", function (e) {
        $("#distrito_empresa").prop("disabled", false);
        e.preventDefault();
        //listarDistrito
        console.log($("#provincia_empresa").val());
        var parametros = new Object();
        parametros.provincia_id = $("#provincia_empresa").val();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "WfEmpleados.aspx/listarDistrito",
            data: JSON.stringify(parametros),
            dataType: "json",
            async: true,
            success: function (data) {
                var todo = "<option value='0'> --Seleccione el distrito-- </option>";
                $.each(data.d, function (key, value) {
                    var parte = "<option value=" + value['distrito_id'] + "> " + value['distrito_nombre'] + " </option>";
                    todo += parte;
                });
                $("#distrito_empresa").html(todo);
            },
            error: function (data) {
                console.info('error');
            }
        });
    });
    $(document).on("change", "#cargo_empresa", function (e) {
        var precio = $("#cargo_empresa option:selected").attr("valorCargo");
        console.log(precio);
        $("#precio_cargo").val(precio);
    });
    solo_letras("#empleado_apellido_paterno");
    solo_letras("#empleado_apellido_materno");
    solo_letras("#empleado_nombre");
    solo_letras("#filtrar_distrito");
    $(document).on("change", "#tipo_de_documento", function (e) {
        e.preventDefault();
        var valor = $(this).val();
        if (valor == "0")
        {
            agrega_error(".valida_p_tipo_documento", "Seleccione el tipo de documento!!");
        } else {
            $("#numero_documento").prop("disabled", false);
            eliminar_error(".valida_p_tipo_documento");
        }
    });
    $(document).on("click", ".siguiente", function (e) {
        var index = $(this).parent().parent().index();
        index++;
        console.log(index);
        if (index == 1)
        {
            $(".registrar_empresa_" + (index + 1)).removeClass("ocultar");
            $(".registrar_empresa_" + (index + 1)).addClass("visible");
            $(".registrar_empresa_" + index).removeClass("visible");
            $(".registrar_empresa_" + index).addClass("ocultar");
            $(".registrar_empresa_2").addClass("slideInRight");
        }
        else if (index == 2)
        {

            var ok = 0;
            if ($("#empleado_apellido_paterno").val().trim().length == 0)
            {
                ok = 1;
                agrega_error(".valida_ap_paterno", "Ingrese el apellido paterno correctamente!!");
            }
            else {
                eliminar_error(".valida_ap_paterno");
            }
            if ($("#empleado_apellido_materno").val().trim().length == 0)
            {
                ok = 1;
                agrega_error(".valida_ap_materno", "Ingrese el apellido materno correctamente!!");
            } else {
                eliminar_error(".valida_ap_materno");
            }
            if ($("#empleado_nombre").val().trim().length == 0)
            {
                ok = 1;
                agrega_error(".valida_pe_nombre","Ingrese el nombre correctamente!!");
            } else
            {
                eliminar_error(".valida_pe_nombre");
            }
            if (ok == 0) {
                $(".registrar_empresa_3").removeClass("slideInLeft");
                $(".registrar_empresa_3").addClass("slideInRight");
                mirar_ocultar(3, 2);
            }
        } else if (index == 3)
        {
            var ok = 0;
            if ($("#nivel_escolaridad").val() == "0")
            {
                ok = 1;
                agrega_error(".valida_p_escolaridad", "Seleccione una escolaridad");
            } else
            {
                eliminar_error(".valida_p_escolaridad");
            }
            if(!$("#telefono_personal").val().trim().match(/^[0-9]+$/))
            {
                ok = 1;
                agrega_error(".valida_p_telefono_personal", "Ingrese el telefono correctamente!!");
            } else
            {
                if ($("#telefono_personal").val().trim().length != 9) {
                    ok = 1;
                    agrega_error(".valida_p_telefono_personal", "Debe tener exactamente 9 digitos!!");
                } else {
                    eliminar_error(".valida_p_telefono_personal");
                }
            }
            if($("#tipo_de_documento").val() == "0")
            {
                ok = 1;
                agrega_error(".valida_p_tipo_documento", "Seleccione el tipo de documento!!");
            } else {
                $("#numero_documento").prop("disabled", false);
                eliminar_error(".valida_p_tipo_documento");
            }
            if (!$("#numero_documento").val().trim().match(/^[0-9]+$/))
            {
                ok = 1;
                agrega_error(".valida_p_n_documento", "Ingrese el documento correctamente!!");
            } else
            {
                var longitud = $("#tipo_de_documento option:selected").attr("longitud");
                console.log(longitud);
                var texto =$("#tipo_de_documento option:selected").html();
                if ($("#numero_documento").val().trim().length != longitud)
                {
                    ok = 1;
                    agrega_error(".valida_p_n_documento", "Ingrese "+texto+" correctamente!!");
                }else{
                    eliminar_error(".valida_p_n_documento");
                }
            }
            if (ok == 0)
            {

                $(".registrar_empresa_4").removeClass("slideInLeft");
                $(".registrar_empresa_4").addClass("slideInRight");
                mirar_ocultar(4, 3);
            }
        } else if (index == 4)
        {
            var ok = 0;
            if ($("#list_departamento").val().trim() == "0") {
                ok = 1;
                agrega_error(".valida_departamento_personal", "Seleccione un departamento!!");
            } else {
                eliminar_error(".valida_departamento_personal");
            }
            if ($("#provincia").val().trim() == "0")
            {
                ok = 1;
                agrega_error(".valida_provincia_personal", "Seleccione la provincia!!");
            } else
            {
                eliminar_error(".valida_provincia_personal");
            }
            if ($("#distrito_id").val().trim() == "0") {
                ok = 1;
                agrega_error(".valida_p_distrito", "El distrito no existes");
            } else {
                eliminar_error(".valida_p_distrito");
            }
            if ($("#direccion_personal").val().trim().length == 0)
            {
                ok = 1;
                agrega_error(".valida_p_direc_personal", "la direccion no debe estar vacia!!");
            } else {
                eliminar_error(".valida_p_direc_personal");
            }
            if (ok == 0)
            {
                $(".registrar_empresa_5").removeClass("slideInLeft");
                $(".registrar_empresa_5").addClass("slideInRight");
                mirar_ocultar(5, 4);
            }
        }
        /*console.log(index);
        $(".registrar_empresa_" + (index+1)).removeClass("ocultar");
        $(".registrar_empresa_" + (index+1)).addClass("visible");
        $(".registrar_empresa_" + index).removeClass("visible");
        $(".registrar_empresa_" + index).addClass("ocultar");*/
    });
    function eliminar_error(variable)
    {
        $(variable).removeClass("errores");
        $(variable).html("");
    }
    function agrega_error(variable,mensaje)
    {
        $(variable).addClass("errores");
        $(variable).html(mensaje);
    }
    function solo_letras(letras)
    {
        $(document).on("keypress", letras, function (key) {
            window.console.log(key.charCode)
            if (key.charCode == 45)
            {
                return false;
            }
            if ((key.charCode < 97 || key.charCode > 122)//letras mayusculas
               && (key.charCode < 65 || key.charCode > 90) //letras minusculas
               && (key.charCode != 45) //retroceso
               && (key.charCode != 241) //ñ
                && (key.charCode != 209) //Ñ
                && (key.charCode != 32) //espacio
                && (key.charCode != 225) //á
                && (key.charCode != 233) //é
                && (key.charCode != 237) //í
                && (key.charCode != 243) //ó
                && (key.charCode != 250) //ú
                && (key.charCode != 193) //Á
                && (key.charCode != 201) //É
                && (key.charCode != 205) //Í
                && (key.charCode != 211) //Ó
                && (key.charCode != 218) //Ú

               )
                return false;
        });
    }
    $(document).on("click", ".anterior", function (e) {
        var index = $(this).parent().parent().index();
        index++;
        console.log(index);
        $(".registrar_empresa_" + (index - 1)).removeClass("slideInRight");
        $(".registrar_empresa_" + (index - 1)).addClass("slideInLeft");
        $(".registrar_empresa_" + (index -1)).removeClass("ocultar");
        $(".registrar_empresa_" + (index -1 )).addClass("visible");
        $(".registrar_empresa_" + index).removeClass("visible");
        $(".registrar_empresa_" + index).addClass("ocultar");
    });
    $(document).on("click", ".lista_empleados", function (e) {
        e.preventDefault();
        var index = $(this).parent().parent().index();
        console.log(index);
        $(".registrar_empresa_"+(index+1)).removeClass("visible");
        $(".registrar_empresa_"+ (index + 1)).addClass("ocultar");
        $(".registrar_empresa_6").removeClass("ocultar");
        $(".registrar_empresa_6").addClass("visible");
    });
    $(document).on("click", "#insertar", function (e) {
        e.preventDefault();
        /*Validaciones*/
        var ok = 0;
        if ($("#cargo_empresa").val()=="0") {
            ok = 1;
            agrega_error(".valida_cargo", "seleccione el cargo!!!");
        } else {
            eliminar_error(".valida_cargo");
        }
        if ($("#area_empresa").val() == "0")
        {
            ok = 1;
            agrega_error(".valida_p_ar_empresa", "Selecione el area!!");
        } else
        {
            eliminar_error(".valida_p_ar_empresa");
        }
        if ($("#listar_sucursales").val() == "0") {
            ok = 1;
            agrega_error(".valida_p_sucursales", "Selecione una sucursal");
        } else {
            eliminar_error(".valida_p_sucursales");
        }
        $("#fecha_ingreso_empresa").attr("min",$("#empleado_fecha_nacimiento").val());
        if (ok == 0) {
            var ape_paterno = $("#empleado_apellido_paterno").val();
            var ape_materno = $("#empleado_apellido_materno").val();
            var nombre = $("#empleado_nombre").val();
            var fecha_nacimiento = $("#empleado_fecha_nacimiento").val();
            var sexo = $("input[name=radio_genero]:checked").val();
            var nivel_escolaridad = $("#nivel_escolaridad").val();
            var telefono_personal = $("#telefono_personal").val();
            var estado_civil = $("#estado_civil").val();
            var tipo_documento = $("#tipo_de_documento").val();
            var numero_documento = $("#numero_documento").val();
            var departamento_personal = $("#list_departamento").val();
            var provincia_personal = $("#provincia").val();
            var distrito_personal = $("#distrito_id").val();
            var direccion_personal = $("#direccion_personal").val();
            var sucursal_id = $("#listar_sucursales").val();
            var salario_personal = $("#salario_personal").val();
            var personal_area = $("#area_empresa").val();
            var remuneracion_extra = $("#remuneraciones_extras").val();
            var fecha_ingreso_personal = $("#fecha_ingreso_empresa").val();
            var planilla = $("input[name=radio_planilla]:checked").val();
            var id_cargo = $("#cargo_empresa").val();
            var parametros = new Object();
            parametros.ape_paterno = ape_paterno;
            parametros.ape_materno = ape_materno;
            parametros.nombre = nombre;
            parametros.fecha_nacimiento = fecha_nacimiento;
            parametros.sexo_personal = sexo;
            parametros.nivel_escolaridad = nivel_escolaridad;
            parametros.telefono_personal = telefono_personal;
            parametros.estado_civil = estado_civil;
            parametros.tipo_documento = tipo_documento;
            parametros.numero_documento = numero_documento;
            parametros.distrito_personal = distrito_personal;
            parametros.direccion_personal = direccion_personal;
            parametros.sucursal_id = sucursal_id;
            parametros.personal_area = personal_area;
            parametros.fecha_ingreso_personal = fecha_ingreso_personal;
            parametros.planilla = planilla;
            parametros.id_cargo = id_cargo;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "WfEmpleados.aspx/inserta_personal",
                data: JSON.stringify(parametros),
                dataType: "json",
                async: true,
                success: function (data) {
                    if (data.d == "2") {
                        $(".valida_si_existe_dni").html("No se pudo guardar el dni ya existe");
                    } else {
                        location.reload();
                    }
                    //console.log(data);
                },
                error: function (data) {
                    console.info('error');
                }
            });
        }
    });
    $(document).on("click", ".cancelar_registro", function (e) {
        location.reload();
    });
    $(document).on("click", "#desea_ingresar_empleado", function (e) {
        e.preventDefault();
        $(".registrar_empresa_1").removeClass("ocultar");
        $(".registrar_empresa_1").addClass("visible");
        $(".registrar_empresa_6").removeClass("visible");
        $(".registrar_empresa_6").addClass("ocultar");
    });
    $(document).on("click", ".editar_personal", function (e) {
        $("#insertar").attr("id", "actualizar");
        $(".registrar_empresa_2").removeClass("slideInLeft");
        $(".registrar_empresa_2").addClass("slideInRight");
        mirar_ocultar(2, 1);
        console.log("entreee");
        var fila = $(this).parent().parent().index();

        fila++;
        /*var ape_paterno = $("#empleado_apellido_paterno").val();
        var ape_materno = $("#empleado_apellido_materno").val();
        var nombre = $("#empleado_nombre").val();
        var fecha_nacimiento = $("#empleado_fecha_nacimiento").val();
        var sexo = $("input[name=radio_genero]:checked").val();
        var sexo_personal = "masculino";
        if (sexo == "2") {
            sexo_personal = "femenino";
        }*/

        $("#numero_documento").prop("disabled", false);
        $("#personal_id").val($("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 1 + ")").html());
        $("#empleado_apellido_paterno").val($("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 2 + ")").html());
        $("#empleado_apellido_materno").val($("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 3 + ")").html());
        $("#empleado_nombre").val($("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 4 + ")").html());
        $("#listar_sucursales").val($("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 8 + ")").html());
        var fecha_naci = convertir_fecha( $("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 16 + ")").html());
        $("#empleado_fecha_nacimiento").val(fecha_naci);
        var valor_sexo = $("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 26 + ")").html();
        var valor_planilla = $("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 27 + ")").html();
        $("#list_departamento").val($("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 30 + ")").html());
        var name_provincia = $("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 29 + ")").html();
        var id_provincia = $("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 28 + ")").html();
        //$("#provincia option:contains(" + name_provincia + ")").attr('selected', 'selected');
        $("#provincia option:selected").text(name_provincia);
        $("#provincia option:selected").attr("value", id_provincia);
        $(".registrar_empresa_2 input[value=" + valor_sexo + "]").prop("checked", true);
        $(".registrar_empresa_5 input[value=" + valor_planilla + "]").prop("checked", true);
        $("#nivel_escolaridad").val($("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 17 + ")").html());
        $("#telefono_personal").val($("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 15 + ")").html());
        $("#tipo_de_documento").val($("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 12 + ")").html());
        $("#numero_documento").val($("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 13 + ")").html());
        $("#distrito_id").val($("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 23 + ")").html());
        $("#filtrar_distrito").val($("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 24 + ")").html());
        $("#direccion_personal").val($("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 14 + ")").html());
        $("#cargo_empresa").val($("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 20 + ")").html());
        //$("#salario_personal").val($("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 21 + ")").html());
        $("#area_empresa").val($("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 10 + ")").html());
        $("#precio_cargo").val($("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 22 + ")").html());
        $("#fecha_ingreso_empresa").val(convertir_fecha($("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 19 + ")").html()));
    });

    function mirar_ocultar(visible,oculto)
    {
        $(".registrar_empresa_" + visible).removeClass("ocultar");
        $(".registrar_empresa_"+visible).addClass("visible");
        $(".registrar_empresa_"+oculto).removeClass("visible");
        $(".registrar_empresa_"+oculto).addClass("ocultar");
    }
    function convertir_fecha(fecha_old)
    {
        var fecha_naci = fecha_old;
        var posi = fecha_naci.indexOf(" ");
        fecha_naci = fecha_naci.substring(0, posi);
        for (var i = 0; i < fecha_naci.length; i++) {
            if (fecha_naci.charAt(i) == '/') {
                var fh = fecha_naci.substring(0, i);
                var lo = fecha_naci.substring(i + 1, fecha_naci.length);
                fecha_naci = fh + "-" + lo;
            }
        }
        var mes = fecha_naci.substring(3, 5);
        var anho = fecha_naci.substring(6, fecha_naci.length);
        fecha_naci = anho + "-" + mes + "-"+ fecha_naci.substring(0, 2);
        return fecha_naci;
    }

   /* var parametros = new Object();
    parametros.pSC = 1
    var list_filtro = [];
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "WfEmpleados.aspx/filtrarDistrito",
        data: JSON.stringify(parametros),
        dataType: "json",
        async: true,
        success: function (data) {
            $("#filtrar_distrito").typeahead('destroy');
            list_filtro = data.d;
            var arreglo = Array();
            $.each($(data.d), function (a, b) {
                arreglo[a] = b.nombreDistrito;
            });
            $("#filtrar_distrito").typeahead({ source: arreglo });
        },
        error: function (data) {
            console.info('error');
        }
    });*/
    $(document).on("change", "#filtrar_distrito", function (e) {
        e.preventDefault();
        var distrito_id = "";
        var ok = 0;
        $.each($(list_filtro), function (a, b) {
            if (b.nombreDistrito == $("#filtrar_distrito").val().trim())
            {
                ok = 1;
                distrito_id = b.IdDistrito;
            }
        });
        if (ok == 1)
        {
            $("#distrito_id").val(distrito_id);
        } else
        {
            $("#distrito_id").val("0");
        }
    });
    $(document).on("click", "#actualizar", function (e) {
        e.preventDefault();
        var ok = 0;
        if ($("#cargo_empresa").val() == "0") {
            ok = 1;
            agrega_error(".valida_cargo", "seleccione el cargo!!!");
        } else {
            eliminar_error(".valida_cargo");
        }
        if ($("#area_empresa").val() == "0") {
            ok = 1;
            agrega_error(".valida_p_ar_empresa", "Selecione el area!!");
        } else {
            eliminar_error(".valida_p_ar_empresa");
        }
        if ($("#listar_sucursales").val() == "0") {
            ok = 1;
            agrega_error(".valida_p_sucursales", "Selecione una sucursal");
        } else {
            eliminar_error(".valida_p_sucursales");
        }
        if (ok == 0) {
            var personal_id = $("#personal_id").val();
            var ape_paterno = $("#empleado_apellido_paterno").val();
            var ape_materno = $("#empleado_apellido_materno").val();
            var nombre = $("#empleado_nombre").val();
            var fecha_nacimiento = $("#empleado_fecha_nacimiento").val();
            var sexo = $("input[name=radio_genero]:checked").val();
            var planilla = $("input[name=radio_planilla]:checked").val();
            var nivel_escolaridad = $("#nivel_escolaridad").val();
            var telefono_personal = $("#telefono_personal").val();
            var estado_civil = $("#estado_civil").val();
            var tipo_documento = $("#tipo_de_documento").val();
            var numero_documento = $("#numero_documento").val();
            /*console.log(nivel_escolaridad);
            console.log(telefono_personal);
            console.log(estado_civil);
            console.log(tipo_documento);
            console.log(numero_documento);*/
            var departamento_personal = $("#list_departamento").val();
            var provincia_personal = $("#provincia").val();
            var distrito_personal = $("#distrito_id").val();
            var direccion_personal = $("#direccion_personal").val();
            /*console.log(departamento_personal);
            console.log(provincia_personal);
            console.log(distrito_personal);
            console.log(direccion_personal);*/

            var sucursal_id = $("#listar_sucursales").val();
            /*console.log(nombre_sucursal);
            console.log(departamento_sucursal);
            console.log(provincia_sucursal);
            console.log(distrito_sucursal);
            console.log(direccion_sucursal);
            console.log(telefono_sucursal);
            console.log(ruc_sucursal);*/
            var id_cargo = $("#cargo_empresa").val();
            var personal_area = $("#area_empresa").val();
            var fecha_ingreso_personal = $("#fecha_ingreso_empresa").val();

            var parametros = new Object();
            parametros.personal_id = personal_id;
            parametros.ape_paterno = ape_paterno;
            parametros.ape_materno = ape_materno;
            parametros.nombre = nombre;
            parametros.fecha_nacimiento = fecha_nacimiento;
            parametros.sexo_personal = sexo;
            parametros.nivel_escolaridad = nivel_escolaridad;
            parametros.telefono_personal = telefono_personal;
            parametros.estado_civil = estado_civil;
            parametros.tipo_documento = tipo_documento;
            parametros.numero_documento = numero_documento;
            parametros.distrito_personal = distrito_personal;
            parametros.direccion_personal = direccion_personal;
            parametros.sucursal_id = sucursal_id;
            parametros.personal_area = personal_area;
            parametros.fecha_ingreso_personal = fecha_ingreso_personal;
            parametros.planilla = planilla;
            parametros.id_cargo = id_cargo;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "WfEmpleados.aspx/actualizarPersonal",
                data: JSON.stringify(parametros),
                dataType: "json",
                async: true,
                success: function (data) {
                    if (data.d == "2") {
                        $(".valida_si_existe_dni").html("No se pudo guardar el dni ya existe");
                    } else {
                        location.reload();
                    }
                    //console.log(data);
                },
                error: function (data) {
                    console.info('error');
                }
            });
        }
    });
    $(document).on("click", ".solo_cerrara_popup", function (e) {
        e.preventDefault();
        cerrar_popup();
    });
    function cerrar_popup()
    {
        $("#solo_cerrar").removeClass("solo_cerrara_popup");
        $("#confirmar_eliminar_popup").html("");

    }
    $(document).on("click", ".cancela_eliminar_personal", function (e) {
        e.preventDefault();
        cerrar_popup();
    });
    $(document).on("click", ".acepta_eliminar_personal", function (e) {
        e.preventDefault();
        fila = $("#popup_personal_id").val();
        var parametros = new Object();
        parametros.personal_id = $("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 1 + ")").html();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "WfEmpleados.aspx/eliminarPersonal",
            data: JSON.stringify(parametros),
            dataType: "json",
            async: true,
            success: function (data) {
                $("#lista_empleados tbody tr:nth-child(" + fila + ")").remove();
                cerrar_popup();
                //location.reload();
            },
            error: function (data) {
                console.info('error');
            }
        });
    });
    $(document).on("click", ".eliminar_personal", function (e) {
        e.preventDefault();
        var fila = $(this).parent().parent().index();
        fila++;
        $("#solo_cerrar").addClass("solo_cerrara_popup");
        var id_persona = $("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 1 + ")").html();
        var id_salario = $("#lista_empleados tbody tr:nth-child(" + fila + ") td:nth-child(" + 20 + ")").html();
        var personal_id = "<input type='hidden' id='popup_personal_id' value='" + fila + "'>";
        var salario_id = "<input type='hidden'id='popup_salariol_id'value='" + id_salario + "'>";
        var imagen_peligro = "<img class='imagen_peligro' src='../imagenes/peligro.png' />";
        var fila_1 = "<h3> Confirmación </h3>";
        var fila_2 = "<h5>¿Desea eliminar al personal?</h5>";
        var boton_aceptar = "<input type='button' class='acepta_eliminar_personal btn btn-primary btn-lg' value='Aceptar'>";
        var boton_cancelar = "<input type='button' class='cancela_eliminar_personal btn btn-danger btn-lg' value='Cancelar'>";
        var fila_3 = "<div>"+boton_aceptar+boton_cancelar+"</div>";
        var section = "<div class='div_padre_popup'>"+fila_1+fila_2+fila_3+"</div>";
        var maquetado = "<div class='clasepopup_eliminar'>" + personal_id + salario_id + imagen_peligro +section + "</div>";
        $("#confirmar_eliminar_popup").html(maquetado);
        
    });
});