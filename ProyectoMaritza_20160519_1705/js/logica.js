$(document).on("ready", function () {

    $(document).on("click", "#fondo_poppup", function (e) {
        $("#fondo_poppup").css({ "display": "none" });
        $(".poppup_personal").css({ "display": "none" });
        $(".poppup_rol").css({ "display": "none" });
        $(".poppup_interfaz").css({ "display": "none" });
        $(".poppup_modulo").css({ "display": "none" });
    });

    $(document).on("click", ".close_poppup", function (e) {
        $("#fondo_poppup").css({ "display": "none" });
        $(".poppup_personal").css({ "display": "none" });
        $(".poppup_rol").css({ "display": "none" });
        $(".poppup_interfaz").css({ "display": "none" });
        $(".poppup_modulo").css({ "display": "none" });
    });

    $(document).on("click", ".botonSeleccionPersonal", function (e) {
        $("#fondo_poppup").css({ "display": "block" });
        $(".poppup_personal").css({ "display": "block" });
    });

    $(document).on("click", ".botonSeleccionRol", function (e) {
        $("#fondo_poppup").css({ "display": "block" });
        $(".poppup_rol").css({ "display": "block" });
    });

    $(document).on("click", ".botonSeleccionModulo", function (e) {
        $("#fondo_poppup").css({ "display": "block" });
        $(".poppup_modulo").css({ "display": "block" });
    });

    $(document).on("click", ".botonSeleccionInterfaces", function (e) {
        $("#fondo_poppup").css({ "display": "block" });
        $(".poppup_interfaz").css({ "display": "block" });
    });

    DiseñoAlert();
    function DiseñoAlert() {
        $("#lblAlert").parent().hide();
        if ($("#lblAlert").attr("class") == "alertDanger") {
            $("#lblAlert").parent().addClass("alert alert-danger").slideToggle(1000);
        }
        else if ($("#lblAlert").attr("class") == "alertSuccess") {
            $("#lblAlert").parent().addClass("alert alert-success").slideToggle(1000);
        }

        $("#ContentPlaceHolder1_lblAlert").parent().hide();
        if ($("#ContentPlaceHolder1_lblAlert").attr("class") == "alertDanger") {
            $("#ContentPlaceHolder1_lblAlert").parent().addClass("alert alert-danger").slideToggle(1000);
        }
        else if ($("#ContentPlaceHolder1_lblAlert").attr("class") == "alertSuccess") {
            $("#ContentPlaceHolder1_lblAlert").parent().addClass("alert alert-success").slideToggle(1000);
        }
    }

    DiseñoActivoInactivo();
    function DiseñoActivoInactivo() {
        $("table input[type=checkbox]").siblings("span:contains('Activo')").css({ "color": "green" });
        $("table input[type=checkbox]").siblings("span:contains('Inactivo')").css({ "color": "red" });
    }

    //-----------------------MODULO---------------//

    function OcultarPoppupModulos() {
        var cantidadPoppupModulo = $("#ContentPlaceHolder1_gdvListarModulos").find("tr").length;
        for (var i = 0; i < cantidadPoppupModulo; i++) {
            if (i == 0) $("#ContentPlaceHolder1_gdvListarModulos").find("tr").eq(i).find("th").eq(0).css({ "display": "none" })
            else $("#ContentPlaceHolder1_gdvListarModulos").find("tr").eq(i).find("td").eq(0).css({ "display": "none" })
        }
    }

    function OcultarGridModulos() {
        var cantidadListaModulo = $("#ContentPlaceHolder1_gdvListaModulos").find("tr").length;
        for (var i = 0; i < cantidadListaModulo; i++) {
            if (i == 0) $("#ContentPlaceHolder1_gdvListaModulos").find("tr").eq(i).find("th").eq(0).css({ "display": "none" })
            else $("#ContentPlaceHolder1_gdvListaModulos").find("tr").eq(i).find("td").eq(0).css({ "display": "none" })
        }
    }

    $(document).on("click", "#ContentPlaceHolder1_gdvListarModulos tr a", function (e) {
        e.preventDefault();
        var indice = $(this).parent().parent().find("td").eq(0).text().trim();
        var modulo = $(this).parent().parent().find("td").eq(2).text().trim();
        $("#ContentPlaceHolder1_txtNombreModulo").val(modulo);
        $("#ContentPlaceHolder1_txtSeleccionModulo").val(indice);
    });

    $(document).on("click", "#ContentPlaceHolder1_gdvListaModulos tr a", function (e) {

        if ($(this).text().trim() == "Editar") {
            e.preventDefault();

            var nombreModificar = $(this).parent().parent().find("td").eq(2).text().trim();
            $("#ContentPlaceHolder1_txtDescripcionModulo").val(nombreModificar);
            var indiceModificar = $(this).parent().parent().find("td").eq(0).text().trim();
            $("#ContentPlaceHolder1_txtIdModificar").val(indiceModificar);

            $("#ContentPlaceHolder1_btnAgregar").attr("disabled", true);
            $("#ContentPlaceHolder1_btnModificar").attr("disabled", false);
            $("#ContentPlaceHolder1_lblAlert").parent().hide();
            $("#ContentPlaceHolder1_lblAlert").text("");
        }
    });

    $(document).on("click", ".FormModulo #ContentPlaceHolder1_btnAgregar", function (e) {
        $(".FormModulo #ContentPlaceHolder1_lblAlert").text("");
        var respuesta = ValidacionesModulo($(".FormModulo #ContentPlaceHolder1_txtDescripcionModulo").val().trim());
        if (respuesta == false) {
            $("#ContentPlaceHolder1_lblAlert").attr("class", "alertDanger");
            DiseñoAlert();
            e.preventDefault();
        }
    });

    $(document).on("click", ".FormModulo #ContentPlaceHolder1_btnModificar", function (e) {
        $(".FormModulo #ContentPlaceHolder1_lblAlert").text("");
        var respuesta = ValidacionesModulo($(".FormModulo #ContentPlaceHolder1_txtDescripcionModulo").val().trim());
        if (respuesta == false) {
            $("#ContentPlaceHolder1_lblAlert").attr("class", "alertDanger");
            DiseñoAlert();
            e.preventDefault();
        }
    });

    function ValidacionesModulo(nombreModulo) {
        var resultado_validacion = true;
        if (nombreModulo.length > 50) {
            $(".FormModulo #ContentPlaceHolder1_lblAlert").append("Máximo 50 caracteres para nombre de Módulo.<br>");
            resultado_validacion = false;
        }
        if (nombreModulo == "") {
            $(".FormModulo #ContentPlaceHolder1_lblAlert").append("Ingrese Nombre de M&oacute;dulo.<br>");
            resultado_validacion = false;
        }
        else if (!nombreModulo.match(/^[a-zA-ZáéíóúàèìòùÀÈÌÒÙÁÉÍÓÚñÑüÜ\s]+$/)) {
            $(".FormModulo #ContentPlaceHolder1_lblAlert").append("Ingrese un Nombre v&aacute;lido.<br>");
            resultado_validacion = false;
        }
        return resultado_validacion;
    }

    $(document).on("keydown", ".FormModulo #ContentPlaceHolder1_txtDescripcionModulo", function (e) {
        if (e.keyCode == 9 || e.keyCode == 20 || e.keyCode == 46 || e.keyCode == 8 || e.keyCode == 13 || e.keyCode == 16 || e.keyCode == 32 || e.keyCode == 37 || e.keyCode == 39 || e.keyCode == 192) {
        }
        else {
            if (e.keyCode < 95) {
                if (e.keyCode < 65 || e.keyCode > 90) {
                    e.preventDefault();
                }
            }
            else {
                if (e.keyCode < 96 || e.keyCode > 105) {
                    e.preventDefault();
                }
            }
        }
    });

    //-----------------------INTERFAZ---------------//
    $(document).on("click", "#ContentPlaceHolder1_gdvListaInterfaz tr a", function (e) {

        if ($(this).text().trim() == "Editar") {
            e.preventDefault();

            var nombreModificar = $(this).parent().parent().find("td").eq(3).text().trim();
            $("#ContentPlaceHolder1_txtDescripcionInterfaz").val(nombreModificar);
            var indiceModificar = $(this).parent().parent().find("td").eq(1).text().trim();
            $("#ContentPlaceHolder1_txtInterfazModificar").val(indiceModificar);
            var nombreModuloPoppup = $(this).parent().parent().find("td").eq(5).text().trim();
            $("#ContentPlaceHolder1_txtNombreModulo").val(nombreModuloPoppup);
            var nombreIdModuloPoppup = $(this).parent().parent().find("td").eq(0).text().trim();
            $("#ContentPlaceHolder1_txtSeleccionModulo").val(nombreIdModuloPoppup);

            $("#ContentPlaceHolder1_btnAgregar").attr("disabled", true);
            $("#ContentPlaceHolder1_btnModificar").attr("disabled", false);
            $("#ContentPlaceHolder1_lblAlert").parent().hide();
            $("#ContentPlaceHolder1_lblAlert").text("");
        }
    });

    $(document).on("change", ".FormInterfaz #ContentPlaceHolder1_gdvListaInterfaz input[type=checkbox]", function (e) {

        var parametro = new Object();
        if ($(this).attr("checked") == "checked") {
            $(this).parent().find("span").text("Activo");
            $(this).parent().find("span").css({ "color": "green" });
            parametro.estadoInterfazNum = "1";
            parametro.idEstadoInterfaz = $(this).parent().parent().find("td").eq(1).text().trim();
        }
        else {
            $(this).parent().find("span").text("Inactivo");
            $(this).parent().find("span").css({ "color": "red" });
            parametro.estadoInterfazNum = "0";
            parametro.idEstadoInterfaz = $(this).parent().parent().find("td").eq(1).text().trim();
        }
        console.log("sssss");
        var identidadInterfaz = this;
        $.ajax({
            url: "RegistroInterfaz.aspx/ModificarEstadoInterfaz",
            type: 'POST',
            data: JSON.stringify(parametro),
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            async: true,
            success: function (data) {
                console.log(data.d);
                if (data.d == "0") {
                    $("#ContentPlaceHolder1_lblAlert").parent().hide();
                    $(".FormInterfaz #ContentPlaceHolder1_lblAlert").text("");
                    resultadoModificacion = 0;
                    //console.log($("#ContentPlaceHolder1_gdvListaInterfaz td:contains('"+ identidadInterfaz +"')").parent().find("span").text("Activo"));
                    $(identidadInterfaz).parent().find("span").text("Activo");
                    $(identidadInterfaz).attr("checked", true);
                    $(".FormInterfaz #ContentPlaceHolder1_lblAlert").text("No se puede desactivar esta interfaz, Interfaz en uso.");
                    $("#ContentPlaceHolder1_lblAlert").attr("class", "alertDanger");
                    DiseñoAlert();
                }
                console.log('sin error');
            },
            error: function (data) {
                console.log('error');
            }
        });
    });

    $(document).on("click", ".FormInterfaz #ContentPlaceHolder1_btnAgregar", function (e) {
        $(".FormInterfaz #ContentPlaceHolder1_lblAlert").text("");
        var respuesta = ValidacionesInterfaz($(".FormInterfaz #ContentPlaceHolder1_txtDescripcionInterfaz").val().trim(), $(".FormInterfaz #ContentPlaceHolder1_txtSeleccionModulo").val().trim());
        if (respuesta == false) {
            $("#ContentPlaceHolder1_lblAlert").attr("class", "alertDanger");
            DiseñoAlert();
            e.preventDefault();
        }
    });

    $(document).on("click", ".FormInterfaz #ContentPlaceHolder1_btnModificar", function (e) {
        $(".FormInterfaz #ContentPlaceHolder1_lblAlert").text("");
        var respuesta = ValidacionesInterfaz($(".FormInterfaz #ContentPlaceHolder1_txtDescripcionInterfaz").val().trim(), $(".FormInterfaz #ContentPlaceHolder1_txtSeleccionModulo").val().trim());
        if (respuesta == false) {
            $("#ContentPlaceHolder1_lblAlert").attr("class", "alertDanger");
            DiseñoAlert();
            e.preventDefault();
        }
    });

    function ValidacionesInterfaz(nombreInterfaz, seleccionModulo) {
        var resultado_validacion = true;
        if (nombreInterfaz.length > 50) {
            $(".FormInterfaz #ContentPlaceHolder1_lblAlert").append("Máximo 50 caracteres para nombre de Interfaz.<br>");
            resultado_validacion = false;
        }
        if (nombreInterfaz == "") {
            $(".FormInterfaz #ContentPlaceHolder1_lblAlert").append("Ingrese Nombre de Interfaz.<br>");
            resultado_validacion = false;
        }
        else if (!nombreInterfaz.match(/^[a-zA-ZáéíóúàèìòùÀÈÌÒÙÁÉÍÓÚñÑüÜ\s]+$/)) {
            $(".FormInterfaz #ContentPlaceHolder1_lblAlert").append("Ingrese un Nombre v&aacute;lido.<br>");
            resultado_validacion = false;
        }
        if (seleccionModulo == "") {
            $(".FormInterfaz #ContentPlaceHolder1_lblAlert").append("Seleccione un M&oacute;dulo.<br>");
            resultado_validacion = false;
        }
        return resultado_validacion;
    }

    $(document).on("keydown", ".FormInterfaz #ContentPlaceHolder1_txtDescripcionInterfaz", function (e) {
        if (e.keyCode == 9 || e.keyCode == 20 || e.keyCode == 46 || e.keyCode == 8 || e.keyCode == 13 || e.keyCode == 16 || e.keyCode == 32 || e.keyCode == 37 || e.keyCode == 39 || e.keyCode == 192) {
        }
        else {
            if (e.keyCode < 95) {
                if (e.keyCode < 65 || e.keyCode > 90) {
                    e.preventDefault();
                }
            }
            else {
                if (e.keyCode < 96 || e.keyCode > 105) {
                    e.preventDefault();
                }
            }
        }
    });

    //-----------------------ROL---------------//
    $(document).on("click", "#ContentPlaceHolder1_gdvListaRol tr a", function (e) {

        if ($(this).text().trim() == "Editar") {
            e.preventDefault();

            var nombreModificar = $(this).parent().parent().find("td").eq(2).text().trim();
            $("#ContentPlaceHolder1_txtDescripcionRol").val(nombreModificar);
            var indiceModificar = $(this).parent().parent().find("td").eq(0).text().trim();
            $("#ContentPlaceHolder1_txtIdModificar").val(indiceModificar);

            var parametro = new Object();
            parametro.IdRolModificar = indiceModificar;

            $.ajax({
                url: "RegistroRol.aspx/ObtenerInterfacesRol",
                type: 'POST',
                data: JSON.stringify(parametro),
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                async: true,
                success: function (data) {
                    for (var i = 1; i < $("#ContentPlaceHolder1_gdvListarPoppupInterfaces tbody tr").length; i++) {
                        $("#ContentPlaceHolder1_gdvListarPoppupInterfaces tbody tr").eq(i).find("td").eq(3).find("input").attr("checked", false);
                    }

                    for (var i = 1; i < $("#ContentPlaceHolder1_gdvListarPoppupInterfaces tbody tr").length; i++) {
                        $.each(data.d, function (key, value) {
                            if ($("#ContentPlaceHolder1_gdvListarPoppupInterfaces tbody tr").eq(i).find("td").eq(0).text().trim() == value.IdInterfaz) {
                                $("#ContentPlaceHolder1_gdvListarPoppupInterfaces tbody tr").eq(i).find("td").eq(3).find("input").attr("checked", true);
                            }
                        });
                    }
                    console.log('sin error');
                    console.log(data.d);
                    console.log(data.d[0].IdInterfaz);
                    $("#ContentPlaceHolder1_btnAgregar").attr("disabled", true);
                    $("#ContentPlaceHolder1_btnModificar").attr("disabled", false);
                    $("#ContentPlaceHolder1_lblAlert").parent().hide();
                    $("#ContentPlaceHolder1_lblAlert").text("");
                },
                error: function (data) {
                    console.log('error');
                }
            });
        }
    });

    $(document).on("click", ".FormRol #ContentPlaceHolder1_btnAgregar", function (e) {
        $(".FormRol #ContentPlaceHolder1_lblAlert").text("");
        var marcado = false;
        for (var i = 1; i < $("#ContentPlaceHolder1_gdvListarPoppupInterfaces tbody tr").length; i++) {
            if ($("#ContentPlaceHolder1_gdvListarPoppupInterfaces tbody tr").eq(i).find("td").eq(3).find("input").attr("checked") == "checked") {
                marcado = true;
            }
        }

        var respuesta = ValidacionesRol($(".FormRol #ContentPlaceHolder1_txtDescripcionRol").val().trim(), marcado);
        if (respuesta == false) {
            $("#ContentPlaceHolder1_lblAlert").attr("class", "alertDanger");
            DiseñoAlert();
            e.preventDefault();
        }
    });

    $(document).on("click", ".FormRol #ContentPlaceHolder1_btnModificar", function (e) {
        $(".FormRol #ContentPlaceHolder1_lblAlert").text("");
        var marcado = false;
        for (var i = 1; i < $("#ContentPlaceHolder1_gdvListarPoppupInterfaces tbody tr").length; i++) {
            if ($("#ContentPlaceHolder1_gdvListarPoppupInterfaces tbody tr").eq(i).find("td").eq(3).find("input").attr("checked") == "checked") {
                marcado = true;
            }
        }

        var respuesta = ValidacionesRol($(".FormRol #ContentPlaceHolder1_txtDescripcionRol").val().trim(), marcado);
        if (respuesta == false) {
            $("#ContentPlaceHolder1_lblAlert").attr("class", "alertDanger");
            DiseñoAlert();
            e.preventDefault();
        }
    });

    function ValidacionesRol(nombreRol, marcadointerfaz) {
        var resultado_validacion = true;
        if (nombreRol.length > 50) {
            $(".FormRol #ContentPlaceHolder1_lblAlert").append("Máximo 50 caracteres para nombre de Rol.<br>");
            resultado_validacion = false;
        }
        if (nombreRol == "") {
            $(".FormRol #ContentPlaceHolder1_lblAlert").append("Ingrese Nombre del Rol.<br>");
            resultado_validacion = false;
        }
        else if (!nombreRol.match(/^[a-zA-ZáéíóúàèìòùÀÈÌÒÙÁÉÍÓÚñÑüÜ\s]+$/)) {
            $(".FormRol #ContentPlaceHolder1_lblAlert").append("Ingrese un Nombre v&aacute;lido.<br>");
            resultado_validacion = false;
        }
        if (marcadointerfaz == false) {
            $(".FormRol #ContentPlaceHolder1_lblAlert").append("Seleccione por lo menos una Interfaz.<br>");
            resultado_validacion = false;
        }
        return resultado_validacion;
    }

    $(document).on("keydown", ".FormRol #ContentPlaceHolder1_txtDescripcionRol", function (e) {
        if (e.keyCode == 9 || e.keyCode == 20 || e.keyCode == 46 || e.keyCode == 8 || e.keyCode == 13 || e.keyCode == 16 || e.keyCode == 32 || e.keyCode == 37 || e.keyCode == 39 || e.keyCode == 192) {
        }
        else {
            if (e.keyCode < 95) {
                if (e.keyCode < 65 || e.keyCode > 90) {
                    e.preventDefault();
                }
            }
            else {
                if (e.keyCode < 96 || e.keyCode > 105) {
                    e.preventDefault();
                }
            }
        }
    });


    //-----------------------Usuario---------------//

    $(document).on("click", "#ContentPlaceHolder1_gdvListarPoppupPersonal tr a", function (e) {
        e.preventDefault();

        var indicePersonal = $(this).parent().parent().find("td").eq(0).text().trim();
        $("#ContentPlaceHolder1_txtIdPersonal").val(indicePersonal);
        var nombrePersonal = $(this).parent().parent().find("td").eq(2).text().trim();
        $("#ContentPlaceHolder1_txtNombresPersonal").val(nombrePersonal);
        var apellidosPersonal = $(this).parent().parent().find("td").eq(3).find("span").eq(0).text().trim() + " " + $(this).parent().parent().find("td").eq(3).find("span").eq(1).text().trim();
        $("#ContentPlaceHolder1_txtApellidosPersonal").val(apellidosPersonal);
        var documentoPersonal = $(this).parent().parent().find("td").eq(4).find("span").eq(0).text().trim() + " : " + $(this).parent().parent().find("td").eq(4).find("span").eq(1).text().trim();
        $("#ContentPlaceHolder1_txtDocumento").val(documentoPersonal);
    });

    $(document).on("click", "#ContentPlaceHolder1_gdvListarPoppupRol tr a", function (e) {
        e.preventDefault();

        var indiceRol = $(this).parent().parent().find("td").eq(0).text().trim();
        $("#ContentPlaceHolder1_txtIdRol").val(indiceRol);
        var nombreRol = $(this).parent().parent().find("td").eq(2).text().trim();
        $("#ContentPlaceHolder1_txtPoppupRol").val(nombreRol);
    });

    $(document).on("click", "#ContentPlaceHolder1_gdvListaUsuarios tr a", function (e) {

        if ($(this).text().trim() == "Editar") {
            e.preventDefault();

            var nombreModificar = $(this).parent().parent().find("td").eq(4).text().trim();
            $("#ContentPlaceHolder1_txtNombreUsuario").val(nombreModificar);
            var indiceModificar = $(this).parent().parent().find("td").eq(2).text().trim();
            $("#ContentPlaceHolder1_txtIdModificar").val(indiceModificar);

            var parametro = new Object();
            parametro.IdUsuarioModificar = indiceModificar;

            $.ajax({
                url: "RegistrarUsuario.aspx/ObtenerDatosUsuario",
                type: 'POST',
                data: JSON.stringify(parametro),
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                async: true,
                success: function (data) {
                    $("#ContentPlaceHolder1_chkModificarContrsena").attr("checked", false);
                    $("#ContentPlaceHolder1_chkModificarContrsena").css({ "display": "inline-block" });
                    $("label[for=ContentPlaceHolder1_chkModificarContrsena]").css({ "display": "inline-block" });

                    $("#ContentPlaceHolder1_txtContrasena").attr("disabled", true);
                    $("#ContentPlaceHolder1_txtConfirmarContrasena").attr("disabled", true);

                    $("#ContentPlaceHolder1_txtContrasena").val("");
                    $("#ContentPlaceHolder1_txtConfirmarContrasena").val("");

                    $("#ContentPlaceHolder1_txtNombresPersonal").val(data.d[0].nombrePersonal);
                    $("#ContentPlaceHolder1_txtApellidosPersonal").val(data.d[0].apellidoPaternoPersonal + " " + data.d[0].apellidoMaternoPersonal);
                    $("#ContentPlaceHolder1_txtDocumento").val(data.d[0].nombreTipoDocumento.trim() + " : " + data.d[0].numeroDocumentoPersonal);
                    $("#ContentPlaceHolder1_txtIdPersonal").val(data.d[0].IdPersonal);

                    $("#ContentPlaceHolder1_txtPoppupRol").val(data.d[0].nombreRol);
                    $("#ContentPlaceHolder1_txtIdRol").val(data.d[0].IdRol);

                    console.log('sin error');
                    console.log(data.d);
                    console.log(data.d[0].IdInterfaz);
                    $("#ContentPlaceHolder1_btnAgregar").attr("disabled", true);
                    $("#ContentPlaceHolder1_btnModificar").attr("disabled", false);
                    $("#ContentPlaceHolder1_lblAlert").parent().hide();
                    $("#ContentPlaceHolder1_lblAlert").text("");
                },
                error: function (data) {
                    console.log('error');
                }
            });
        }
    });

    //$(document).on("click", "#ContentPlaceHolder1_btnModificar", function (e) {

    //    if ($("#ContentPlaceHolder1_txtContrasena").attr("disabled") != "disabled" && $("#ContentPlaceHolder1_txtConfirmarContrasena").attr("disabled") != "disabled") {

    //        if ($("#ContentPlaceHolder1_txtContrasena").val() != $("#ContentPlaceHolder1_txtConfirmarContrasena").val()) {
    //            e.preventDefault();
    //            $("#ContentPlaceHolder1_lblAlert").append("Confirme correctamente su contrase&ntilde;a.<br>");
    //        }
    //        else if ($("#ContentPlaceHolder1_txtContrasena").val() == "" && $("#ContentPlaceHolder1_txtConfirmarContrasena").val() == "") {
    //            e.preventDefault();
    //            $("#ContentPlaceHolder1_lblAlert").append("Llene los datos de contrase&ntilde;a y confirmación.<br>");
    //        }
    //    }
    //});

    $(document).on("click", "#ContentPlaceHolder1_chkModificarContrsena", function (e) {
        if ($(this).attr('checked') == "checked") {
            $("#ContentPlaceHolder1_txtContrasena").attr("disabled", false);
            $("#ContentPlaceHolder1_txtConfirmarContrasena").attr("disabled", false);
        }
        else {
            $("#ContentPlaceHolder1_txtContrasena").attr("disabled", true);;
            $("#ContentPlaceHolder1_txtConfirmarContrasena").attr("disabled", true);
        }
    });

    $(document).on("click", "#ContentPlaceHolder1_gdvListaUsuarios input[type=checkbox]", function (e) {

        var parametro = new Object();
        if ($(this).attr("checked") == "checked") {
            $(this).parent().find("span").text("Activo");
            $(this).parent().find("span").css({ "color": "green" });
            parametro.estadoUsuarioNum = "1";
            parametro.idEstadoUsuario = $(this).parent().parent().find("td").eq(2).text().trim();
        }
        else {
            $(this).parent().find("span").text("Inactivo");
            $(this).parent().find("span").css({ "color": "red" });
            parametro.estadoUsuarioNum = "0";
            parametro.idEstadoUsuario = $(this).parent().parent().find("td").eq(2).text().trim();
        }

        $.ajax({
            url: "RegistrarUsuario.aspx/ModificarEstadoUsuario",
            type: 'POST',
            data: JSON.stringify(parametro),
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            async: true,
            success: function (data) {
                console.log('sin error');
            },
            error: function (data) {
                console.log('error');
            }
        });
    });


    $(document).on("click", ".FormUsuario #ContentPlaceHolder1_btnAgregar", function (e) {
        $(".FormUsuario #ContentPlaceHolder1_lblAlert").text("");

        var respuesta = ValidacionesUsuarioAgregar($(".FormUsuario #ContentPlaceHolder1_txtNombreUsuario").val().trim(),
            $(".FormUsuario #ContentPlaceHolder1_txtIdPersonal").val().trim(),
            $(".FormUsuario #ContentPlaceHolder1_txtIdRol").val().trim(),
            $(".FormUsuario #ContentPlaceHolder1_txtContrasena").val().trim(),
            $(".FormUsuario #ContentPlaceHolder1_txtConfirmarContrasena").val().trim());
        if (respuesta == false) {
            $("#ContentPlaceHolder1_lblAlert").attr("class", "alertDanger");
            DiseñoAlert();
            e.preventDefault();
        }
    });

    $(document).on("click", ".FormUsuario #ContentPlaceHolder1_btnModificar", function (e) {
        $(".FormUsuario #ContentPlaceHolder1_lblAlert").text("");
        var verificarpassword = true;
        var respuesta = ValidacionesUsuarioModificar($(".FormUsuario #ContentPlaceHolder1_txtNombreUsuario").val().trim(),
            $(".FormUsuario #ContentPlaceHolder1_txtIdPersonal").val().trim(),
            $(".FormUsuario #ContentPlaceHolder1_txtIdRol").val().trim());

        if ($("#ContentPlaceHolder1_txtContrasena").attr("disabled") != "disabled" && $("#ContentPlaceHolder1_txtConfirmarContrasena").attr("disabled") != "disabled") {

            if (!$("#ContentPlaceHolder1_txtContrasena").val().trim().match(/^[A-Za-z0-9]+$/)) {
                $(".FormUsuario #ContentPlaceHolder1_lblAlert").append("Ingrese una Contraseña v&aacute;lido (n&uacute;meros o letras).<br>");
                verificarpassword = false;
            }

            if ($("#ContentPlaceHolder1_txtContrasena").val().trim() != $("#ContentPlaceHolder1_txtConfirmarContrasena").val().trim() && $("#ContentPlaceHolder1_txtConfirmarContrasena").val().trim() != "") {
                $("#ContentPlaceHolder1_lblAlert").append("Confirme correctamente su contrase&ntilde;a.<br>");
                verificarpassword = false;
            }
            else if ($("#ContentPlaceHolder1_txtContrasena").val().trim() == "" && $("#ContentPlaceHolder1_txtConfirmarContrasena").val().trim() == "") {
                $("#ContentPlaceHolder1_lblAlert").append("Llene los datos de contrase&ntilde;a y confirmaci&oacute;n.<br>");
                verificarpassword = false;
            }
        }

        if ($("#ContentPlaceHolder1_txtContrasena").val().trim().length != 10 && $("#ContentPlaceHolder1_txtContrasena").val().trim() != "") {
            $(".FormUsuario #ContentPlaceHolder1_lblAlert").append("La contrase&ntilde;a debe tener 10 caracteres.<br>");
            verificarpassword = false;
        }

        if (respuesta == false || verificarpassword == false) {
            $("#ContentPlaceHolder1_lblAlert").attr("class", "alertDanger");
            DiseñoAlert();
            e.preventDefault();
        }
    });

    function ValidacionesUsuarioAgregar(nombreUsuario, IdPersonal, IdRol, contrasena, ConfirmacionContrasena) {
        var resultado_validacion = true;
        if (nombreUsuario.length > 40) {
            $(".FormUsuario #ContentPlaceHolder1_lblAlert").append("Máximo 40 caracteres para nombre de Usuario.<br>");
            resultado_validacion = false;
        }
        if (nombreUsuario == "") {
            $(".FormUsuario #ContentPlaceHolder1_lblAlert").append("Ingrese Nombre del Usuario.<br>");
            resultado_validacion = false;
        }
        else if (!nombreUsuario.match(/^[A-Za-z0-9áéíóúàèìòùÀÈÌÒÙÁÉÍÓÚñÑüÜ]+$/)) {
            $(".FormUsuario #ContentPlaceHolder1_lblAlert").append("Ingrese un Nombre de Usuario v&aacute;lido.<br>");
            resultado_validacion = false;
        }
        if (IdPersonal == "") {
            $(".FormUsuario #ContentPlaceHolder1_lblAlert").append("Seleccione un Personal.<br>");
            resultado_validacion = false;
        }
        if (IdRol == "") {
            $(".FormUsuario #ContentPlaceHolder1_lblAlert").append("Seleccione un Rol.<br>");
            resultado_validacion = false;
        }
        if (contrasena == "") {
            $(".FormUsuario #ContentPlaceHolder1_lblAlert").append("Ingrese Contrase&ntilde;a.<br>");
            resultado_validacion = false;
        }
        else if (!contrasena.match(/^[A-Za-z0-9]+$/)) {
            $(".FormUsuario #ContentPlaceHolder1_lblAlert").append("Ingrese una Contrase&ntilde;a v&aacute;lida (n&uacute;meros o letras).<br>");
            resultado_validacion = false;
        }
        if (ConfirmacionContrasena == "") {
            $(".FormUsuario #ContentPlaceHolder1_lblAlert").append("Ingrese Confirmaci&oacute;n de Contrase&ntilde;a.<br>");
            resultado_validacion = false;
        }
        if (contrasena != ConfirmacionContrasena && ConfirmacionContrasena != "") {
            $(".FormUsuario #ContentPlaceHolder1_lblAlert").append("Confirme Correctamente su Contrase&ntilde;a.<br>");
            resultado_validacion = false;
        }
        if (contrasena.length != 10 && contrasena != "") {
            $(".FormUsuario #ContentPlaceHolder1_lblAlert").append("La contrase&ntilde;a debe tener 10 caracteres.<br>");
            resultado_validacion = false;
        }
        return resultado_validacion;
    }

    function ValidacionesUsuarioModificar(nombreUsuario, IdPersonal, IdRol) {
        var resultado_validacion_modificar = true;
        if (nombreUsuario.length > 40) {
            $(".FormUsuario #ContentPlaceHolder1_lblAlert").append("Máximo 40 caracteres para nombre de Usuario.<br>");
            resultado_validacion_modificar = false;
        }
        if (nombreUsuario == "") {
            $(".FormUsuario #ContentPlaceHolder1_lblAlert").append("Ingrese Nombre del Usuario.<br>");
            resultado_validacion_modificar = false;
        }
        else if (!nombreUsuario.match(/^[A-Za-z0-9áéíóúàèìòùÀÈÌÒÙÁÉÍÓÚñÑüÜ]+$/)) {
            $(".FormUsuario #ContentPlaceHolder1_lblAlert").append("Ingrese un Nombre de Usuario v&aacute;lido.<br>");
            resultado_validacion_modificar = false;
        }
        if (IdPersonal == "") {
            $(".FormUsuario #ContentPlaceHolder1_lblAlert").append("Seleccione un Personal.<br>");
            resultado_validacion_modificar = false;
        }
        if (IdRol == "") {
            $(".FormUsuario #ContentPlaceHolder1_lblAlert").append("Seleccione un Rol.<br>");
            resultado_validacion_modificar = false;
        }
        return resultado_validacion_modificar;
    }

    //$(document).on("keydown", ".FormUsuario #ContentPlaceHolder1_txtNombreUsuario", function (e) {
    //    if (e.keyCode == 9 || e.keyCode == 20 || e.keyCode == 46 || e.keyCode == 8 || e.keyCode == 13 || e.keyCode == 16 || e.keyCode == 32 || e.keyCode == 37 || e.keyCode == 39 || e.keyCode == 192) {
    //    }
    //    else {
    //        if (e.keyCode < 95) {
    //            if (e.keyCode < 65 || e.keyCode > 90) {
    //                e.preventDefault();
    //            }
    //        }
    //        else {
    //            if (e.keyCode < 96 || e.keyCode > 105) {
    //                e.preventDefault();
    //            }
    //        }
    //    }
    //});

    $(document).on("click", ".FormUsuario #ContentPlaceHolder1_btnCancelar", function (e) {
        $(".FormUsuario #ContentPlaceHolder1_txtContrasena").val("");
        $(".FormUsuario #ContentPlaceHolder1_txtConfirmarContrasena").val("");
    });

    //-----------------------Logueo---------------//

    $(document).on("click", "#formLogueo #btnLogueo", function (e) {
        $("#lblAlert").text("");
        if ($("#txtUsuario").val().trim().length > 40) {
            e.preventDefault();
            $("#lblAlert").append("Máximo 40 caracteres para nombre de Usuario.<br>");
        }
        if ($("#txtUsuario").val().trim() == "") {
            e.preventDefault();
            $("#lblAlert").append("Ingrese nombre de Usuario.<br>");
        }
        else if (!$("#txtUsuario").val().trim().match(/^[A-Za-z0-9áéíóúàèìòùÀÈÌÒÙÁÉÍÓÚñÑüÜ]+$/)) {
            e.preventDefault();
            $("#lblAlert").append("Ingrese un Nombre de Usuario v&aacute;lido.<br>");
        }
        if ($("#txtPassword").val().trim() == "") {
            e.preventDefault();
            $("#lblAlert").append("Ingrese Contrase&ntilde;a.<br>");
        }
        else if (!$("#txtPassword").val().trim().match(/^[A-Za-z0-9]+$/)) {
            $("#lblAlert").append("Ingrese una Contrase&ntilde;a v&aacute;lida (n&uacute;meros o letras).<br>");
            e.preventDefault();
        }
        if ($("#txtPassword").val().trim().length != 10 && $("#txtPassword").val().trim() != "") {
            e.preventDefault();
            $("#lblAlert").append("La contrase&ntilde;a debe tener 10 caracteres.<br>");
        }


        if ($("#lblAlert").text() != "") {
            $("#lblAlert").attr("class", "alertDanger");
            DiseñoAlert();
        }
    });


    //-----------------------Cambiar de Contraseña---------------//

    /*-----------------------------------------------------------------*/
    $(document).on("click", ".FormModificarContrasena #ContentPlaceHolder1_btnModificar", function (e) {
        $("#ContentPlaceHolder1_lblAlert").text("");
        if ($("#ContentPlaceHolder1_txtPasswordAnterior").val().trim() == "") {
            e.preventDefault();
            $("#ContentPlaceHolder1_lblAlert").append("Ingrese Antigua Contraseña.<br>");
        }
        else if (!$("#ContentPlaceHolder1_txtPasswordAnterior").val().trim().match(/^[A-Za-z0-9]+$/)) {
            $("#ContentPlaceHolder1_lblAlert").append("Ingrese una Antigua Contrase&ntilde;a v&aacute;lida (n&uacute;meros o letras).<br>");
            e.preventDefault();
        }
        if ($("#ContentPlaceHolder1_txtNuevoPassword").val().trim() == "") {
            e.preventDefault();
            $("#ContentPlaceHolder1_lblAlert").append("Ingrese Nueva Contraseña.<br>");
        }
        else if (!$("#ContentPlaceHolder1_txtNuevoPassword").val().trim().match(/^[A-Za-z0-9]+$/)) {
            $("#ContentPlaceHolder1_lblAlert").append("Ingrese una Nueva Contrase&ntilde;a v&aacute;lida (n&uacute;meros o letras).<br>");
            e.preventDefault();
        }
        if ($("#ContentPlaceHolder1_txtConfirmarNuevoPassword").val().trim() == "") {
            e.preventDefault();
            $("#ContentPlaceHolder1_lblAlert").append("Confirme Nueva Contrase&ntilde;a.<br>");
        }
        if ($("#ContentPlaceHolder1_txtNuevoPassword").val().trim() != $("#ContentPlaceHolder1_txtConfirmarNuevoPassword").val().trim() && $("#ContentPlaceHolder1_txtConfirmarNuevoPassword").val().trim() != "") {
            e.preventDefault();
            $("#ContentPlaceHolder1_lblAlert").append("Confirme correctamente la Nueva Contrase&ntilde;a.<br>");
        }
        if ($("#ContentPlaceHolder1_txtNuevoPassword").val().trim().length != 10 && $("#ContentPlaceHolder1_txtNuevoPassword").val().trim() != "") {
            e.preventDefault();
            $("#ContentPlaceHolder1_lblAlert").append("La Nueva Contrase&ntilde;a debe tener 10 caracteres.<br>");
        }


        if ($("#ContentPlaceHolder1_lblAlert").text() != "") {
            $("#ContentPlaceHolder1_lblAlert").attr("class", "alertDanger");
            DiseñoAlert();
        }
    });
    /*-----------------------------------------------------------------*/

    $(document).on("click", ".FormModificarContrasena #ContentPlaceHolder1_btnCancelar", function (e) {
        $(".FormModificarContrasena #ContentPlaceHolder1_txtNuevoPassword").val("");
        $(".FormModificarContrasena #ContentPlaceHolder1_txtConfirmarNuevoPassword").val("");
    });
});


