var DriveAccessController = {
    funValidateDriveAccessForm: function () {

        $("#frmtblDriveAccess").validate({
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
        var DriveAccessFormId = $("#DriveAccessFormId").val();
        if ($("#requestFomsVM_IsPending").val() == 'True') {

            if (DriveAccessFormId == 0) {
                $(".clsProcessSteps").hide();
                $(".clsDepartmentHead").hide();
                $(".clsInfraHead").hide();
                $(".clsServerAdmin").hide();
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
                    $('#Reason').attr('readonly', true);
                    $('#Path').attr('readonly', true);
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemarks').attr('readonly', true);
                    $('#SARemarks').attr('readonly', true);
                    $(".ApproveClass").hide();
                    $(".HideClass").hide();
                }


                if (PendingWith == "Department Head") {
                    $('#OnBehalfof').attr('readonly', true);
                    $('#Location').attr('disabled', true);
                    $('#Reason').attr('readonly', true);
                    $('#Path').attr('readonly', true);
                    $(".clsInfraHead").hide();
                    $(".clsServerAdmin").hide();
                    $(".HideClass").hide();
                    $(".clsclosebtn").hide();

                }
                else if (PendingWith == "Infra Head") {
                    $('#OnBehalfof').attr('readonly', true);
                    $('#Location').attr('disabled', true);
                    $('#Reason').attr('readonly', true);
                    $('#Path').attr('readonly', true);
                    $('#DHRemarks').attr('readonly', true);
                    $(".clsServerAdmin").hide();
                    $(".HideClass").hide();
                    $(".clsclosebtn").hide();
                }
                else if (PendingWith == "Server Admin") {
                    $('#OnBehalfof').attr('readonly', true);
                    $('#Location').attr('disabled', true);
                    $('#Reason').attr('readonly', true);
                    $('#Path').attr('readonly', true);
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemarks').attr('readonly', true);
                    $(".HideClass").hide();
                    $(".clsapprovebtn").hide();
                    //$(".clsReturnbtn").hide();
                    //$(".clsRejectbtn").hide();

                }
                else if (PendingWith == "Initiator" && Status == "Returned") {
                    //$(".clsProcessSteps").hide();
                    //$(".clsDepartmentHead").hide();
                    //$(".clsInfraHead").hide();
                    //$(".clsServerAdmin").hide();
                    //$(".ApproveClass").hide();

                    if (ReturnRejectWith == "Department Head") {
                        $('#DHRemarks').attr('readonly', true);
                        $(".clsInfraHead").hide();
                        $(".clsServerAdmin").hide();
                        $(".HideClass").show();
                        $(".clsclosebtn").hide();
                        $(".clsapprovebtn").hide();
                        $(".clsRejectbtn").hide();
                        $(".clsReturnbtn").hide();

                    }
                    else if (ReturnRejectWith == "Infra Head") {
                        $('#DHRemarks').attr('readonly', true);
                        $('#ITDHRemarks').attr('readonly', true);
                        $(".clsServerAdmin").hide();
                        $(".HideClass").show();
                        $(".clsclosebtn").hide();
                        $(".clsapprovebtn").hide();
                        $(".clsRejectbtn").hide();
                        $(".clsReturnbtn").hide();                       
                    }
                    else if (ReturnRejectWith == "Server Admin") {
                        $('#DHRemarks').attr('readonly', true);
                        $('#ITDHRemarks').attr('readonly', true);
                        $('#SARemarks').attr('readonly', true);
                        $(".HideClass").show();
                        $(".clsapprovebtn").hide();
                        $(".clsRejectbtn").hide();
                        $(".clsReturnbtn").hide();
                        $(".clsclosebtn").hide();
                        //$(".clsReturnbtn").hide();
                        //$(".clsRejectbtn").hide();

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
            if (DriveAccessFormId == 0) {
                $(".clsProcessSteps").hide();
                $(".clsDepartmentHead").hide();
                $(".clsInfraHead").hide();
                $(".clsServerAdmin").hide();
                $(".ApproveClass").hide();
            }
            else if (PendingWith == "Initiator" && Status == "Returned") {
                if (ReturnRejectWith == "Department Head") {
                    $('#DHRemarks').attr('readonly', true);
                    $(".clsInfraHead").hide();
                    $(".clsServerAdmin").hide();
                    $(".HideClass").show();
                    $(".clsclosebtn").hide();
                    $(".clsRejectbtn").hide();
                    $(".clsReturnbtn").hide();
                    $(".clsclosebtn").hide();
                    $(".ApproveClass").hide();
                }
                else if (ReturnRejectWith == "Infra Head") {
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemarks').attr('readonly', true);
                    $(".clsServerAdmin").hide();
                    $(".HideClass").show();
                    $(".clsclosebtn").hide();
                    $(".clsRejectbtn").hide();
                    $(".clsReturnbtn").hide();
                    $(".clsclosebtn").hide();
                    $(".ApproveClass").hide();
                }
                else if (ReturnRejectWith == "Server Admin") {
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemarks').attr('readonly', true);
                    $('#SARemarks').attr('readonly', true);
                    $(".HideClass").show();
                    $(".clsapprovebtn").hide();
                    $(".clsRejectbtn").hide();
                    $(".clsReturnbtn").hide();
                    $(".clsclosebtn").hide();
                    $(".ApproveClass").hide();
                    //$(".clsReturnbtn").hide();
                    //$(".clsRejectbtn").hide();

                }
            }
            else if (Status == "Reject") {
                if (ReturnRejectWith == "Department Head") {
                    $('#DHRemarks').attr('readonly', true);
                    $(".clsInfraHead").hide();
                    $(".clsServerAdmin").hide();
                    $(".HideClass").hide();
                    $(".clsclosebtn").hide();
                    $(".clsRejectbtn").hide();
                    $(".clsReturnbtn").hide();
                    $(".clsclosebtn").hide();
                    $(".ApproveClass").hide();
                }
                else if (ReturnRejectWith == "Infra Head") {
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemarks').attr('readonly', true);
                    $(".clsServerAdmin").hide();
                    $(".HideClass").hide();
                    $(".clsclosebtn").hide();
                    $(".clsRejectbtn").hide();
                    $(".clsReturnbtn").hide();
                    $(".clsclosebtn").hide();
                    $(".ApproveClass").hide();
                }
                else if (ReturnRejectWith == "Server Admin") {
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemarks').attr('readonly', true);
                    $('#SARemarks').attr('readonly', true);
                    $(".HideClass").hide();
                    $(".clsapprovebtn").hide();
                    $(".clsRejectbtn").hide();
                    $(".clsReturnbtn").hide();
                    $(".clsclosebtn").hide();
                    $(".ApproveClass").hide();
                    //$(".clsReturnbtn").hide();
                    //$(".clsRejectbtn").hide();

                }
            }
            else {
                $('#OnBehalfof').attr('readonly', true);
                $('#Location').attr('disabled', true);
                $('#Reason').attr('readonly', true);
                $('#Path').attr('readonly', true);
                $('#DHRemarks').attr('readonly', true);
                $('#ITDHRemarks').attr('readonly', true);
                $('#SARemarks').attr('readonly', true);
                $(".HideClass").hide();
                $(".ApproveClass").hide();
            }
        }
    },
    funSubmitDriveAccessFormValue: function () {
        DriveAccessController.funValidateDriveAccessForm();

        if ($("#frmtblDriveAccess").valid()) {
            $('#btnSubmit').attr('disabled', true);
            $.ajax({

                url: glbParamObject.Virutalpath + '/DriveAccess/SubmitDriveAccessFormValue',
                data: $("#frmtblDriveAccess").serialize(),
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

                            window.location.href = glbParamObject.Virutalpath + '/DriveAccess/DriveAcessForms'
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

    funApproveDriveAccessFormValue: function () {
        DriveAccessController.funValidateDriveAccessForm();

        if ($("#frmtblDriveAccess").valid()) {
            $.ajax({

                url: glbParamObject.Virutalpath + '/DriveAccess/ApproveDriveAccessFormValue',
                data: $("#frmtblDriveAccess").serialize(),
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

                            window.location.href = glbParamObject.Virutalpath + '/DriveAccess/DriveAcessForms'
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


    funCloseDriveAccessFormValue: function () {
        DriveAccessController.funValidateDriveAccessForm();

        if ($("#frmtblDriveAccess").valid()) {
            $.ajax({

                url: glbParamObject.Virutalpath + '/DriveAccess/CloseDriveAccessFormValue',
                data: $("#frmtblDriveAccess").serialize(),
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

                            window.location.href = glbParamObject.Virutalpath + '/DriveAccess/DriveAcessForms'
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

    funReturnDriveAccessFormValue: function () {
        $.ajax({

            url: glbParamObject.Virutalpath + '/DriveAccess/ReturnDriveAccessFormValue',
            data: $("#frmtblDriveAccess").serialize(),
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

                        window.location.href = glbParamObject.Virutalpath + '/DriveAccess/DriveAcessForms'
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

    funRejectDriveAccessFormValue: function () {
        $.ajax({

            url: glbParamObject.Virutalpath + '/DriveAccess/RejectDriveAccessFormValue',
            data: $("#frmtblDriveAccess").serialize(),
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

                        window.location.href = glbParamObject.Virutalpath + '/DriveAccess/DriveAcessForms'
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

    funGetProcessSteps: function () {
        var DriveAccessFormId = $("#DriveAccessFormId").val();
        if (DriveAccessFormId !== "0") {
            $.ajax({

                url: glbParamObject.Virutalpath + '/DriveAccess/getStepCount',
                data: { paramDriveAccessFormId: DriveAccessFormId },
                type: 'POST',
                dataType: 'json',
                success: function (responce) {
                    var SuccessCount = responce.length;
                    var liString = "";
                    liString = "<ul class='progressbar'>";
                    for (var i = 1; i < 5; i++) {
                        if (i <= SuccessCount) {
                            var takeiData = i - 1;
                            liString = liString + "<li class='active'>" + responce[takeiData].StatusName + "<br/> Applied by: <br/>" + responce[takeiData].CreateByName + "</li>";
                        }
                        else {
                            liString = liString + "<li class='pending'>Step " + i + "</li>"
                        }
                    }
                    liString = liString + "</ul>";
                    $("#ProcessDiv").append(liString);
                },
                error: function (xxx) {

                }
            });

        }
        else { }
    }
}