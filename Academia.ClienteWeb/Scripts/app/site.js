$(document).ready(function () {
    $(".sdos").select2();
    $("#btn_Buscar").click();
});

$("#btn_Buscar").click(function () {
    $("#btn_Buscar").prop('disabled', true);

    let gId = $("#s_grimorio").val();

    if (gId == '') {
        gId = '0';
    }

    $.ajax({
        type: "GET",
        url: self.urls.obtenerSolicitudes,
        contentType: 'application/json',
        dataType: "html",
        data: {
            grimorioId: gId
        },
        beforeSend: function () {
            $('#modalSpinnerLoader').modal({ backdrop: 'static', keyboard: false })
            $('#modalSpinnerLoader').modal('show');
        },
        success: function (result) {
            $("#contenido").html(result);
        },
        complete: function () {
            $('#modalSpinnerLoader').modal('hide');
            $("#btn_Buscar").prop('disabled', false);
        },
        error: function () {

        }
    });
});

$("#btn_Guardar").click(function () {
    $("#btn_Guardar").prop('disabled', true);
    let modelForm = recolectarYObtenerFormulario();

    $.ajax({
        type: "POST",
        url: self.urls.GuardarSolicitudes,
        contentType: 'application/json',
        dataType: "json",
        data: JSON.stringify({
            registro: modelForm
        }),
        beforeSend: function () {
            $('#modalSpinnerLoader').modal({ backdrop: 'static', keyboard: false })
            $('#modalSpinnerLoader').modal('show');
        },
        success: function (result) {
            if (result.Success) {
                let msj = '';

                for (var i = 0; i < result.Mensajes.length; i++) {
                    msj += result.Mensajes[i] + '\n'
                }
                alert(msj)
                $("#btn_Buscar").click();
            } else {
                let msj = '';

                for (var i = 0; i < result.Mensajes.length; i++) {
                    msj += result.Mensajes[i] + '\n'
                }
                alert(msj)
            }            
        },
        complete: function () {
            $('#modalSpinnerLoader').modal('hide');
            $("#btn_Guardar").prop('disabled', false);
        },
        error: function () {

        }
    });
});

$("#contenido").on('click', '.eliminar', function () {
    let id = $(this).attr("data-iden");

    $.ajax({
        type: "POST",
        url: self.urls.eliminarSolicitudes,
        contentType: 'application/json',
        dataType: "json",
        data: JSON.stringify({
            id: id
        }),
        beforeSend: function () {
            $('#modalSpinnerLoader').modal({ backdrop: 'static', keyboard: false })
            $('#modalSpinnerLoader').modal('show');
        },
        success: function (result) {
            if (result.Success) {
                alert(result.Mensajes[0])
                $("#btn_Buscar").click();
            } else {
                let msj = '';

                for (var i = 0; i < result.Mensajes.length; i++) {
                    msj += result.Mensajes[i] + '\n'
                }
                alert(msj)
            }
        },
        complete: function () {
            $('#modalSpinnerLoader').modal('hide');
            $("#btn_Buscar").prop('disabled', false);
        },
        error: function () {
            alert('Sin conexión')
        }
    });

});

$("#contenido").on('click', '.guardarRow', function () {
    var result = confirm("Comfirmas que deseas guardar todos los cambios de la fila");
    if (result == true) {
        let id = $(this).attr("data-iden");
        let modelo = recolectarYObtenerRow(id);

        $.ajax({
            type: "POST",
            url: self.urls.GuardarSolicitudes,
            contentType: 'application/json',
            dataType: "json",
            data: JSON.stringify({
                registro: modelo
            }),
            beforeSend: function () {
                $('#modalSpinnerLoader').modal({ backdrop: 'static', keyboard: false })
                $('#modalSpinnerLoader').modal('show');
            },
            success: function (result) {
                if (result.Success) {
                    alert(result.Mensajes[0])
                    $("#btn_Buscar").click();
                } else {
                    let msj = '';

                    for (var i = 0; i < result.Mensajes.length; i++) {
                        msj += result.Mensajes[i] + '\n'
                    }
                    alert(msj)
                }       
            },
            complete: function () {
                $('#modalSpinnerLoader').modal('hide');
                $("#btn_Buscar").prop('disabled', false);
            },
            error: function () {
                alert('Sin conexión')
            }
        });
    }
});

function recolectarYObtenerFormulario() {
    var model = {
        Identificador: 0,
        Nombre: $(`#ipt-nombre`).val(),
        Apellido: $(`#ipt-apellido`).val(),
        Creacion: '',
        CodigoIdentificacion: $(`#ipt-identificacion`).val(),
        Edad: $(`#ipt-edad`).val(),
        AfinidadMagia: $(`#ipt-afinidad`).val(),
        Grimoio: '',
        Estatus: 0
    }
    
    return model;
}

function recolectarYObtenerRow(id) {
    var model = {
        Identificador: id,
        Nombre: $(`#Nombre_${id}`).val(),
        Apellido: $(`#Apellido_${id}`).val(),
        Creacion: '',
        CodigoIdentificacion: $(`#Codigo_${id}`).val(),
        Edad: $(`#Edad_${id}`).val(),
        AfinidadMagia: $(`#Afinidad_${id}`).val(),
        Grimoio: '',
        Estatus: $(`#Estatus_${id}`).val()
    }

    return model;
}