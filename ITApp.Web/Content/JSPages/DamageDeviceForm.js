var DamageDeviceController = {
    funValidateDamageDeviceForm: function () {

        $("#frmtblDamageDevice").validate({
            // Rules for form validation
            onkeyup: false,
            rules: {
                Location: {
                    required: true,
                    //|Data Already exist Kalpesh End|
                },


            },
            // Messages for form validation
            messages: {
                Location: {
                    required: 'Please select location',
                },

            },
            // Do not change code below
            errorPlacement: function (error, element) {
                error.insertAfter(element.parent());
            }
        });
    },
    GetfunDocumentInit: function () {
        var DamageDeviceFormId = $("#DamageDeviceFormId").val();
        if ($("#requestFomsVM_IsPending").val() == 'True') {
            if (DamageDeviceFormId == 0) {
                $(".clsProcessSteps").hide();
                $(".clsDepartmentHead").hide();
                $(".clsITHelpDeskAdmin").hide();
                $(".clsITHelpDeskone").hide();
                $(".clsITHelpDesktwo").hide();
                $(".ApproveClass").hide();
            }
            else {
                var PendingWith = $("#requestFomsVM_PendingWith").val();
                var Status = $("#requestFomsVM_Status").val();              
                var ReturnRejectWith = $("#requestFomsVM_ReturnRejectWith").val();
                var LastPendingWith = $("#requestFomsVM_LastPendingWith").val();
                if (PendingWith == "" && Status == "Close") {
                    $('#OnBehalfof').attr('readonly', true);
                    $('#Location').attr('disabled', true);
                    $('#DamagedDeviceName').attr('readonly', true);
                    $('#DamagedDeviceNo').attr('readonly', true);
                    $('#DamageReason').attr('readonly', true);
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITHelpdeskAdminRemarks').attr('readonly', true);
                    $('#ITHelpdeskRemarks').attr('readonly', true);
                    $('#DStatus').attr('disabled', true);
                    $(".ApproveClass").hide();
                    $(".HideClass").hide();
                }


                if (PendingWith == "Department Head") {
                    $('#OnBehalfof').attr('readonly', true);
                    $('#Location').attr('disabled', true);
                    $('#DamagedDeviceName').attr('readonly', true);
                    $('#DamagedDeviceNo').attr('readonly', true);
                    $('#DamageReason').attr('readonly', true);
                    $(".clsITHelpDeskAdmin").hide();
                    $(".clsITHelpDeskone").hide();
                    $(".clsITHelpDesktwo").hide();
                    $(".HideClass").hide();
                    $(".clsclosebtn").hide();

                }
                else if (PendingWith == "IT Helpdesk Admin") {
                    $('#OnBehalfof').attr('readonly', true);
                    $('#Location').attr('disabled', true);
                    $('#DamagedDeviceName').attr('readonly', true);
                    $('#DamagedDeviceNo').attr('readonly', true);
                    $('#DamageReason').attr('readonly', true);
                    $('#DHRemarks').attr('readonly', true);
                    $(".clsITHelpDeskone").hide();
                    $(".clsITHelpDesktwo").hide();
                    $(".HideClass").hide();
                    $(".clsclosebtn").hide();
                }
                else if (PendingWith == "IT Helpdesk") {
                    $('#OnBehalfof').attr('readonly', true);
                    $('#Location').attr('disabled', true);
                    $('#DamagedDeviceName').attr('readonly', true);
                    $('#DamagedDeviceNo').attr('readonly', true);
                    $('#DamageReason').attr('readonly', true);
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITHelpdeskAdminRemarks').attr('readonly', true);
                    $(".HideClass").hide();
                    $(".clsapprovebtn").hide();
                    $(".clsReturnbtn").hide();
                    $(".clsRejectbtn").hide();

                }
                else if (PendingWith == "Initiator" && Status == "Returned") {                   
                    if (ReturnRejectWith == "Department Head") { 
                        $('#DHRemarks').attr('readonly', true);
                        $(".clsITHelpDeskAdmin").hide();
                        $(".clsITHelpDeskone").hide();
                        $(".clsITHelpDesktwo").hide();
                        $(".HideClass").show();
                        $(".clsclosebtn").hide();
                        $(".clsapprovebtn").hide();
                        $(".clsRejectbtn").hide();
                        $(".clsReturnbtn").hide();

                    }
                    else if (ReturnRejectWith == "IT Helpdesk Admin") {
                      
                        $('#DHRemarks').attr('readonly', true);
                        $('#ITHelpdeskAdminRemarks').attr('readonly', true);
                        $(".clsITHelpDeskone").hide();
                        $(".clsITHelpDesktwo").hide();
                        $(".HideClass").show();
                        $(".clsclosebtn").hide();
                        $(".clsapprovebtn").hide();
                        $(".clsRejectbtn").hide();
                        $(".clsReturnbtn").hide();
                    }
                    else if (ReturnRejectWith == "IT Helpdesk") {                       
                        $('#DHRemarks').attr('readonly', true);
                        $('#ITHelpdeskAdminRemarks').attr('readonly', true);
                        $('#ITHelpdeskRemarks').attr('readonly', true);
                        $('#DStatus').attr('readonly', true);
                        $(".HideClass").show();
                        $(".clsapprovebtn").hide();
                        $(".clsReturnbtn").hide();
                        $(".clsRejectbtn").hide();                       

                    }
                }
            }

            $('.i-checks').on('ifChecked', function (event) {
                if ('IsAccept' == this.id) {
                    $(".HideClass").show();
                }
            });
            $('.i-checks').on('ifUnchecked', function (event) {
                if ('IsAccept' == this.id) {
                    $(".HideClass").hide();
                }
            });
        }
        else {           
            var PendingWith = $("#requestFomsVM_PendingWith").val();
            var ReturnRejectWith = $("#requestFomsVM_ReturnRejectWith").val();
            var Status = $("#requestFomsVM_Status").val();
            var LastPendingWith = $("#requestFomsVM_LastPendingWith").val();
            if (DamageDeviceFormId == 0) {
                $(".clsProcessSteps").hide();
                $(".clsDepartmentHead").hide();
                $(".clsITHelpDeskAdmin").hide();
                $(".clsITHelpDeskone").hide();
                $(".clsITHelpDesktwo").hide();
                $(".ApproveClass").hide();
            }
            else if (PendingWith == "Initiator" && Status == "Returned") {
              
                if (ReturnRejectWith == "Department Head") {
                    $('#DHRemarks').attr('readonly', true);
                    $(".clsITHelpDeskAdmin").hide();
                    $(".clsITHelpDeskone").hide();
                    $(".clsITHelpDesktwo").hide();
                    $(".HideClass").show();
                    $(".clsclosebtn").hide();
                    $(".clsapprovebtn").hide();
                    $(".clsRejectbtn").hide();
                    $(".clsReturnbtn").hide();

                }
                else if (ReturnRejectWith == "IT Helpdesk Admin") {

                    $('#DHRemarks').attr('readonly', true);
                    $('#ITHelpdeskAdminRemarks').attr('readonly', true);
                    $(".clsITHelpDeskone").hide();
                    $(".clsITHelpDesktwo").hide();
                    $(".HideClass").show();
                    $(".clsclosebtn").hide();
                    $(".clsapprovebtn").hide();
                    $(".clsRejectbtn").hide();
                    $(".clsReturnbtn").hide();
                }
                else if (ReturnRejectWith == "IT Helpdesk") {
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITHelpdeskAdminRemarks').attr('readonly', true);
                    $('#ITHelpdeskRemarks').attr('readonly', true);
                    $('#DStatus').attr('readonly', true);
                    $(".HideClass").show();
                    $(".clsapprovebtn").hide();
                    $(".clsReturnbtn").hide();
                    $(".clsRejectbtn").hide();

                }
            }
            else if (Status == "Reject") {
                if (ReturnRejectWith == "Department Head") {
                    $('#DHRemarks').attr('readonly', true);
                    $(".clsITHelpDeskAdmin").hide();
                    $(".clsITHelpDeskone").hide();
                    $(".clsITHelpDesktwo").hide();
                    $(".HideClass").hide();
                    $(".clsclosebtn").hide();
                    $(".clsapprovebtn").hide();
                    $(".clsRejectbtn").hide();
                    $(".clsReturnbtn").hide();

                }
                else if (ReturnRejectWith == "IT Helpdesk Admin") {

                    $('#DHRemarks').attr('readonly', true);
                    $('#ITHelpdeskAdminRemarks').attr('readonly', true);
                    $(".clsITHelpDeskone").hide();
                    $(".clsITHelpDesktwo").hide();
                    $(".HideClass").hide();
                    $(".clsclosebtn").hide();
                    $(".clsapprovebtn").hide();
                    $(".clsRejectbtn").hide();
                    $(".clsReturnbtn").hide();
                }
                else if (ReturnRejectWith == "IT Helpdesk") {
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITHelpdeskAdminRemarks').attr('readonly', true);
                    $('#ITHelpdeskRemarks').attr('readonly', true);
                    $('#DStatus').attr('readonly', true);
                    $(".HideClass").hide();
                    $(".clsapprovebtn").hide();
                    $(".clsReturnbtn").hide();
                    $(".clsRejectbtn").hide();

                }
            }
            else {
                $('#OnBehalfof').attr('readonly', true);
                $('#Location').attr('disabled', true);
                $('#DamagedDeviceName').attr('readonly', true);
                $('#DamagedDeviceNo').attr('readonly', true);
                $('#DamageReason').attr('readonly', true);
                $('#DHRemarks').attr('readonly', true);
                $('#ITHelpdeskAdminRemarks').attr('readonly', true);
                $('#ITHelpdeskRemarks').attr('readonly', true);
                $('#DStatus').attr('disabled', true);
                $(".HideClass").hide();
                $(".ApproveClass").hide();
            }
        }
    },
    funSubmitDamageDeviceFormValue: function () {
        DamageDeviceController.funValidateDamageDeviceForm();

        if ($("#frmtblDamageDevice").valid()) {
            $('#btnSubmit').attr('disabled', true);
            $.ajax({

                url: glbParamObject.Virutalpath + '/DamageDevice/SubmitDamageDeviceFormValue',
                data: $("#frmtblDamageDevice").serialize(),
                type: 'POST',
                dataType: 'json',
                success: function (responce) {
                    $("#divAlert").html("");
                    if (responce.Result == "OK") {
                        //toastr.success(responce.Message);
                        //window.setTimeout(function () {
                        //    window.location.href = glbParamObject.Virutalpath + '/Home/Index'
                        //}, 2000);
                        swal({
                            title: responce.Message.split('|')[0],
                            text: responce.Message.split('|')[1],
                        });
                        window.setTimeout(function () {
                            window.location.href = glbParamObject.Virutalpath + '/Home/Index'
                        }, 3000);
                    }
                    else if (responce.Result == "DUPLICATE") {
                        $("#" + responce.OtherParameter.AlertFieldName).focus();
                        $("#divAlert").html(responce.OtherParameter.AlertMessage);
                        $("#divAlert").show();
                    }
                    else if (responce.Result == "WARNING") {
                        $("#divAlert").html(responce.OtherParameter.AlertMessage);
                        $("#divAlert").show();
                        window.setTimeout(function () {

                            window.location.href = glbParamObject.Virutalpath + '/DamageDevice/DamageDeviceForms'
                        }, 5000);
                    }
                    else if (responce.Result == "ERROR") {
                        //$("#divAlert").html(responce.OtherParameter.AlertMessage);
                        //$("#divAlert").show();
                        toastr.error(responce.Message);
                    }
                },
                error: function (xxx) {

                }
            });
        }
        else {
            /* $("#btnSubmit").ladda().ladda('stop');*/
        }

    },

    funApproveDamageDeviceFormValue: function () {
        DamageDeviceController.funValidateDamageDeviceForm();

        if ($("#frmtblDamageDevice").valid()) {
            $.ajax({

                url: glbParamObject.Virutalpath + '/DamageDevice/ApproveDamageDeviceFormValue',
                data: $("#frmtblDamageDevice").serialize(),
                type: 'POST',
                dataType: 'json',
                success: function (responce) {
                    $("#divAlert").html("");
                    if (responce.Result == "OK") {
                        toastr.success(responce.Message);
                        window.setTimeout(function () {
                            window.location.href = glbParamObject.Virutalpath + '/Home/Index'
                        }, 2000);
                    }
                    else if (responce.Result == "DUPLICATE") {
                        $("#" + responce.OtherParameter.AlertFieldName).focus();
                        $("#divAlert").html(responce.OtherParameter.AlertMessage);
                        $("#divAlert").show();
                    }
                    else if (responce.Result == "WARNING") {
                        $("#divAlert").html(responce.OtherParameter.AlertMessage);
                        $("#divAlert").show();
                        window.setTimeout(function () {

                            window.location.href = glbParamObject.Virutalpath + '/DamageDevice/DamageDeviceForms'
                        }, 5000);
                    }
                    else if (responce.Result == "ERROR") {
                        //$("#divAlert").html(responce.OtherParameter.AlertMessage);
                        //$("#divAlert").show();
                        toastr.error(responce.Message);
                    }
                },
                error: function (xxx) {

                }
            });
        }
        else {
            /* $("#btnSubmit").ladda().ladda('stop');*/
        }
    },


    funCloseDamageDeviceFormValue: function () {
        DamageDeviceController.funValidateDamageDeviceForm();

        if ($("#frmtblDamageDevice").valid()) {
            $.ajax({

                url: glbParamObject.Virutalpath + '/DamageDevice/CloseDamageDeviceFormValue',
                data: $("#frmtblDamageDevice").serialize(),
                type: 'POST',
                dataType: 'json',
                success: function (responce) {
                    $("#divAlert").html("");
                    if (responce.Result == "OK") {
                        toastr.success(responce.Message);
                        window.setTimeout(function () {
                            window.location.href = glbParamObject.Virutalpath + '/Home/Index'
                        }, 2000);
                    }
                    else if (responce.Result == "DUPLICATE") {
                        $("#" + responce.OtherParameter.AlertFieldName).focus();
                        $("#divAlert").html(responce.OtherParameter.AlertMessage);
                        $("#divAlert").show();
                    }
                    else if (responce.Result == "WARNING") {
                        $("#divAlert").html(responce.OtherParameter.AlertMessage);
                        $("#divAlert").show();
                        window.setTimeout(function () {

                            window.location.href = glbParamObject.Virutalpath + '/DamageDevice/DamageDeviceForms'
                        }, 5000);
                    }
                    else if (responce.Result == "ERROR") {
                        //$("#divAlert").html(responce.OtherParameter.AlertMessage);
                        //$("#divAlert").show();
                        toastr.error(responce.Message);
                    }
                },
                error: function (xxx) {

                }
            });
        }
        else {
            /* $("#btnSubmit").ladda().ladda('stop');*/
        }
    },

    funReturnDamageDeviceFormValue: function () {
        $.ajax({

            url: glbParamObject.Virutalpath + '/DamageDevice/ReturnDamageDeviceFormValue',
            data: $("#frmtblDamageDevice").serialize(),
            type: 'POST',
            dataType: 'json',
            success: function (responce) {
                $("#divAlert").html("");
                if (responce.Result == "OK") {
                    toastr.success(responce.Message);
                    window.setTimeout(function () {
                        window.location.href = glbParamObject.Virutalpath + '/Home/Index'
                    }, 2000);
                }
                else if (responce.Result == "DUPLICATE") {
                    $("#" + responce.OtherParameter.AlertFieldName).focus();
                    $("#divAlert").html(responce.OtherParameter.AlertMessage);
                    $("#divAlert").show();
                }
                else if (responce.Result == "WARNING") {
                    $("#divAlert").html(responce.OtherParameter.AlertMessage);
                    $("#divAlert").show();
                    window.setTimeout(function () {

                        window.location.href = glbParamObject.Virutalpath + '/DamageDevice/DamageDeviceForms'
                    }, 5000);
                }
                else if (responce.Result == "ERROR") {
                    //$("#divAlert").html(responce.OtherParameter.AlertMessage);
                    //$("#divAlert").show();
                    toastr.error(responce.Message);
                }
            },
            error: function (xxx) {

            }
        });
    },

    funRejectDamageDeviceFormValue: function () {
        $.ajax({

            url: glbParamObject.Virutalpath + '/DamageDevice/RejectDamageDeviceFormValue',
            data: $("#frmtblDamageDevice").serialize(),
            type: 'POST',
            dataType: 'json',
            success: function (responce) {
                $("#divAlert").html("");
                if (responce.Result == "OK") {
                    toastr.success(responce.Message);
                    window.setTimeout(function () {
                        window.location.href = glbParamObject.Virutalpath + '/Home/Index'
                    }, 2000);
                }
                else if (responce.Result == "DUPLICATE") {
                    $("#" + responce.OtherParameter.AlertFieldName).focus();
                    $("#divAlert").html(responce.OtherParameter.AlertMessage);
                    $("#divAlert").show();
                }
                else if (responce.Result == "WARNING") {
                    $("#divAlert").html(responce.OtherParameter.AlertMessage);
                    $("#divAlert").show();
                    window.setTimeout(function () {

                        window.location.href = glbParamObject.Virutalpath + '/DamageDevice/DamageDeviceForms'
                    }, 5000);
                }
                else if (responce.Result == "ERROR") {
                    //$("#divAlert").html(responce.OtherParameter.AlertMessage);
                    //$("#divAlert").show();
                    toastr.error(responce.Message);
                }
            },
            error: function (xxx) {

            }
        });
    },
   
}