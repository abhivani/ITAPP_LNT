var USBRequestController = {
    funValidateUSBRequestForm: function () {

        $("#frmtblUSBRequest").validate({
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
        var USBRequestFormId = $("#USBRequestFormId").val();
        if ($("#requestFomsVM_IsPending").val() == 'True') {
            var USBRequestFormId = $("#USBRequestFormId").val();
            if (USBRequestFormId == 0) {

                $(".clsProcessSteps").hide();
                $(".HideClass").hide();
                $(".clsDepartmentHead").hide();
                $(".clsItDepartmentHead").hide();
                $(".ApproveClass").hide();
                $(".clsItDepartmentHead").hide();
                $(".clsHelpdesk").hide();
                $(".clsClose").hide();
                $(".CloseClass").hide();

            }
            else {
                var PendingWith = $("#requestFomsVM_PendingWith").val();
                var ReturnRejectWith = $("#requestFomsVM_ReturnRejectWith").val();
                var Status = $("#requestFomsVM_Status").val();
                var LastPendingWith = $("#requestFomsVM_LastPendingWith").val();

                if (PendingWith == "" && Status == "Close") {
                    $('#Reason').attr('readonly', true);
                    $('#Location').attr('disabled', true);
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemarks').attr('readonly', true);
                    $('#SerialNo').attr('readonly', true);
                    $('#Model').attr('readonly', true);
                    $('#Vendor').attr('readonly', true);
                    $('#Description').attr('readonly', true);
                    $('#OTP').attr('readonly', true);
                    $('#Remarks').attr('readonly', true);
                    $('.clsdis').attr('disabled', true);
                    $(".HideClass").hide();
                    $(".ApproveClass").hide();
                    $(".CloseClass").hide();
                }


                if (PendingWith == "Department Head") {
                    $('#Reason').attr('readonly', true);
                    $('#Location').attr('disabled', true);
                    $('.clsdis').attr('disabled', true);
                    $(".clsItDepartmentHead").hide();
                    $(".clsHelpdesk").hide();
                    $(".clsClose").hide();
                    $(".HideClass").hide();
                    $(".ApproveClass").show();
                    $(".CloseClass").hide();
                }
                else if (PendingWith == "IT Department Head") {
                    $('#Reason').attr('readonly', true);
                    $('#Location').attr('disabled', true);
                    $('#DHRemarks').attr('readonly', true);
                    $('.clsdis').attr('disabled', true);
                    $(".clsHelpdesk").hide();
                    $(".clsClose").hide();
                    $(".HideClass").hide();
                    $(".ApproveClass").show();
                    $(".CloseClass").hide();
                }
                else if (PendingWith == "IT Helpdesk" && Status == "Pending IT Helpdesk Approval") {
                    $('#Reason').attr('readonly', true);
                    $('#Location').attr('disabled', true);
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemarks').attr('readonly', true);
                    $('.clsdis').attr('disabled', true);
                    $(".clsClose").hide();
                    $(".HideClass").hide();
                    $(".ApproveClass").show();
                    $(".CloseClass").hide();
                }
                else if (PendingWith == "IT Security") {
                    $('#Reason').attr('readonly', true);
                    $('#Location').attr('disabled', true);
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemarks').attr('readonly', true);
                    $('#SerialNo').attr('readonly', true);
                    $('#Model').attr('readonly', true);
                    $('#Vendor').attr('readonly', true);
                    $('#Description').attr('readonly', true);
                    $('.clsdis').attr('disabled', true);
                    $(".clsClose").hide();
                    $(".HideClass").hide();
                    $(".ApproveClass").show();
                    $(".CloseClass").hide();
                }
                else if (PendingWith == "IT Helpdesk" && Status == "Collection Pending") {
                    $('#Reason').attr('readonly', true);
                    $('#Location').attr('disabled', true);
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemarks').attr('readonly', true);
                    $('#SerialNo').attr('readonly', true);
                    $('#Model').attr('readonly', true);
                    $('#Vendor').attr('readonly', true);
                    $('#Description').attr('readonly', true);
                    $('.clsdis').attr('disabled', true);
                    $(".HideClass").hide();
                    $(".ApproveClass").hide();
                    $(".CloseClass").show();

                }
                else if (PendingWith == "Initiator" && Status == "Returned") {
                    //  else if (PendingWith == "Initiator") {
                    //$(".clsProcessSteps").hide();
                    //$(".clsDepartmentHead").hide();
                    //$(".clsItDepartmentHead").hide();
                    //$(".ApproveClass").hide();
                    //$(".clsItDepartmentHead").hide();
                    //$(".clsHelpdesk").hide();
                    //$(".clsClose").hide();
                    //$(".CloseClass").hide();

                    if (ReturnRejectWith == "Department Head") {
                        $('#DHRemarks').attr('readonly', true);
                        $('.clsdis').attr('disabled', true);
                        $(".clsItDepartmentHead").hide();
                        $(".clsHelpdesk").hide();
                        $(".clsClose").hide();
                        $(".HideClass").show();
                        $(".ApproveClass").hide();
                        $(".CloseClass").hide();
                    }
                    else if (ReturnRejectWith == "IT Department Head") {

                        $('#DHRemarks').attr('readonly', true);
                        $('#ITDHRemarks').attr('readonly', true);
                        $('.clsdis').attr('disabled', true);
                        $(".clsHelpdesk").hide();
                        $(".clsClose").hide();
                        $(".HideClass").show();
                        $(".ApproveClass").hide();
                        $(".CloseClass").hide();
                    }
                    else if (ReturnRejectWith == "IT Helpdesk" && LastPendingWith == "Pending IT Helpdesk Approval") {

                        $('#DHRemarks').attr('readonly', true);
                        $('#ITDHRemarks').attr('readonly', true);
                        $('#SerialNo').attr('readonly', true);
                        $('#Model').attr('readonly', true);
                        $('#Vendor').attr('readonly', true);
                        $('#Description').attr('readonly', true);
                        $('.clsdis').attr('disabled', true);
                        $(".clsClose").hide();
                        $(".HideClass").show();
                        $(".ApproveClass").hide();
                        $(".CloseClass").hide();
                    }
                    else if (ReturnRejectWith == "IT Security") {

                        $('#DHRemarks').attr('readonly', true);
                        $('#ITDHRemarks').attr('readonly', true);
                        $('#SerialNo').attr('readonly', true);
                        $('#Model').attr('readonly', true);
                        $('#Vendor').attr('readonly', true);
                        $('#Description').attr('readonly', true);
                        $('.clsdis').attr('disabled', true);
                        $(".clsClose").hide();
                        $(".HideClass").show();
                        $(".ApproveClass").hide();
                        $(".CloseClass").hide();
                    }
                    else if (ReturnRejectWith == "IT Helpdesk" && LastPendingWith == "Collection Pending") {
                        $('#DHRemarks').attr('readonly', true);
                        $('#ITDHRemarks').attr('readonly', true);
                        $('#SerialNo').attr('readonly', true);
                        $('#Model').attr('readonly', true);
                        $('#Vendor').attr('readonly', true);
                        $('#Description').attr('readonly', true);
                        $('.clsdis').attr('disabled', true);
                        $(".HideClass").show();
                        $(".ApproveClass").hide();
                        $(".CloseClass").hide();

                    }
                }
                else if (Status == "Reject") {
                    if (ReturnRejectWith == "Department Head") {
                        $('#DHRemarks').attr('readonly', true);
                        $('.clsdis').attr('disabled', true);
                        $(".clsItDepartmentHead").hide();
                        $(".clsHelpdesk").hide();
                        $(".clsClose").hide();
                        $(".HideClass").hide();
                        $(".ApproveClass").hide();
                        $(".CloseClass").hide();
                    }
                    else if (ReturnRejectWith == "IT Department Head") {

                        $('#DHRemarks').attr('readonly', true);
                        $('#ITDHRemarks').attr('readonly', true);
                        $('.clsdis').attr('disabled', true);
                        $(".clsHelpdesk").hide();
                        $(".clsClose").hide();
                        $(".HideClass").hide();
                        $(".ApproveClass").hide();
                        $(".CloseClass").hide();
                    }
                    else if (ReturnRejectWith == "IT Helpdesk" && LastPendingWith == "Pending IT Helpdesk Approval") {

                        $('#DHRemarks').attr('readonly', true);
                        $('#ITDHRemarks').attr('readonly', true);
                        $('#SerialNo').attr('readonly', true);
                        $('#Model').attr('readonly', true);
                        $('#Vendor').attr('readonly', true);
                        $('#Description').attr('readonly', true);
                        $('.clsdis').attr('disabled', true);
                        $(".clsClose").hide();
                        $(".HideClass").hide();
                        $(".ApproveClass").hide();
                        $(".CloseClass").hide();
                    }
                    else if (ReturnRejectWith == "IT Security") {

                        $('#DHRemarks').attr('readonly', true);
                        $('#ITDHRemarks').attr('readonly', true);
                        $('#SerialNo').attr('readonly', true);
                        $('#Model').attr('readonly', true);
                        $('#Vendor').attr('readonly', true);
                        $('#Description').attr('readonly', true);
                        $('.clsdis').attr('disabled', true);
                        $(".clsClose").hide();
                        $(".HideClass").hide();
                        $(".ApproveClass").hide();
                        $(".CloseClass").hide();
                    }
                    else if (ReturnRejectWith == "IT Helpdesk" && LastPendingWith == "Collection Pending") {
                        $('#DHRemarks').attr('readonly', true);
                        $('#ITDHRemarks').attr('readonly', true);
                        $('#SerialNo').attr('readonly', true);
                        $('#Model').attr('readonly', true);
                        $('#Vendor').attr('readonly', true);
                        $('#Description').attr('readonly', true);
                        $('.clsdis').attr('disabled', true);
                        $(".HideClass").hide();
                        $(".ApproveClass").hide();
                        $(".CloseClass").hide();

                    }
                }
             
            }


        }
        else {
            var PendingWith = $("#requestFomsVM_PendingWith").val();
            var ReturnRejectWith = $("#requestFomsVM_ReturnRejectWith").val();
            var Status = $("#requestFomsVM_Status").val();
            var LastPendingWith = $("#requestFomsVM_LastPendingWith").val();
            if (USBRequestFormId == 0) {
                $(".clsProcessSteps").hide();
                $(".HideClass").hide();
                $(".clsDepartmentHead").hide();
                $(".clsItDepartmentHead").hide();
                $(".ApproveClass").hide();
                $(".clsItDepartmentHead").hide();
                $(".clsHelpdesk").hide();
                $(".clsClose").hide();
                $(".CloseClass").hide();

            }
            else if (PendingWith == "Initiator" && Status == "Returned") {
                //  else if (PendingWith == "Initiator") {
                //$(".clsProcessSteps").hide();
                //$(".clsDepartmentHead").hide();
                //$(".clsItDepartmentHead").hide();
                //$(".ApproveClass").hide();
                //$(".clsItDepartmentHead").hide();
                //$(".clsHelpdesk").hide();
                //$(".clsClose").hide();
                //$(".CloseClass").hide();
                if (ReturnRejectWith == "Department Head") {
                    $('#DHRemarks').attr('readonly', true);
                    $('.clsdis').attr('disabled', true);
                    $(".clsItDepartmentHead").hide();
                    $(".clsHelpdesk").hide();
                    $(".clsClose").hide();
                    $(".HideClass").show();
                    $(".ApproveClass").hide();
                    $(".CloseClass").hide();
                }
                else if (ReturnRejectWith == "IT Department Head") {

                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemarks').attr('readonly', true);
                    $('.clsdis').attr('disabled', true);
                    $(".clsHelpdesk").hide();
                    $(".clsClose").hide();
                    $(".HideClass").show();
                    $(".ApproveClass").hide();
                    $(".CloseClass").hide();
                }
                else if (ReturnRejectWith == "IT Helpdesk" && LastPendingWith == "Pending IT Helpdesk Approval") {

                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemarks').attr('readonly', true);
                    $('#SerialNo').attr('readonly', true);
                    $('#Model').attr('readonly', true);
                    $('#Vendor').attr('readonly', true);
                    $('#Description').attr('readonly', true);
                    $('.clsdis').attr('disabled', true);
                    $(".clsClose").hide();
                    $(".HideClass").show();
                    $(".ApproveClass").hide();
                    $(".CloseClass").hide();
                }
                else if (ReturnRejectWith == "IT Security") {

                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemarks').attr('readonly', true);
                    $('#SerialNo').attr('readonly', true);
                    $('#Model').attr('readonly', true);
                    $('#Vendor').attr('readonly', true);
                    $('#Description').attr('readonly', true);
                    $('.clsdis').attr('disabled', true);
                    $(".clsClose").hide();
                    $(".HideClass").show();
                    $(".ApproveClass").hide();
                    $(".CloseClass").hide();
                }
                else if (ReturnRejectWith == "IT Helpdesk" && LastPendingWith == "Collection Pending") {
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemarks').attr('readonly', true);
                    $('#SerialNo').attr('readonly', true);
                    $('#Model').attr('readonly', true);
                    $('#Vendor').attr('readonly', true);
                    $('#Description').attr('readonly', true);
                    $('.clsdis').attr('disabled', true);
                    $(".HideClass").show();
                    $(".ApproveClass").hide();
                    $(".CloseClass").hide();

                }
            }
            else if (Status == "Reject") {
                if (ReturnRejectWith == "Department Head") {
                    $('#DHRemarks').attr('readonly', true);
                    $('.clsdis').attr('disabled', true);
                    $(".clsItDepartmentHead").hide();
                    $(".clsHelpdesk").hide();
                    $(".clsClose").hide();
                    $(".HideClass").hide();
                    $(".ApproveClass").hide();
                    $(".CloseClass").hide();
                }
                else if (ReturnRejectWith == "IT Department Head") {

                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemarks').attr('readonly', true);
                    $('.clsdis').attr('disabled', true);
                    $(".clsHelpdesk").hide();
                    $(".clsClose").hide();
                    $(".HideClass").hide();
                    $(".ApproveClass").hide();
                    $(".CloseClass").hide();
                }
                else if (ReturnRejectWith == "IT Helpdesk" && LastPendingWith == "Pending IT Helpdesk Approval") {

                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemarks').attr('readonly', true);
                    $('#SerialNo').attr('readonly', true);
                    $('#Model').attr('readonly', true);
                    $('#Vendor').attr('readonly', true);
                    $('#Description').attr('readonly', true);
                    $('.clsdis').attr('disabled', true);
                    $(".clsClose").hide();
                    $(".HideClass").hide();
                    $(".ApproveClass").hide();
                    $(".CloseClass").hide();
                }
                else if (ReturnRejectWith == "IT Security") {

                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemarks').attr('readonly', true);
                    $('#SerialNo').attr('readonly', true);
                    $('#Model').attr('readonly', true);
                    $('#Vendor').attr('readonly', true);
                    $('#Description').attr('readonly', true);
                    $('.clsdis').attr('disabled', true);
                    $(".clsClose").hide();
                    $(".HideClass").hide();
                    $(".ApproveClass").hide();
                    $(".CloseClass").hide();
                }
                else if (ReturnRejectWith == "IT Helpdesk" && LastPendingWith == "Collection Pending") {
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemarks').attr('readonly', true);
                    $('#SerialNo').attr('readonly', true);
                    $('#Model').attr('readonly', true);
                    $('#Vendor').attr('readonly', true);
                    $('#Description').attr('readonly', true);
                    $('.clsdis').attr('disabled', true);
                    $(".HideClass").hide();
                    $(".ApproveClass").hide();
                    $(".CloseClass").hide();

                }
            }
            else {
                $('#Reason').attr('readonly', true);
                $('#Location').attr('disabled', true);
                $('#DHRemarks').attr('readonly', true);
                $('#ITDHRemarks').attr('readonly', true);
                $('#SerialNo').attr('readonly', true);
                $('#Model').attr('readonly', true);
                $('#Vendor').attr('readonly', true);
                $('#Description').attr('readonly', true);
                $('#OTP').attr('readonly', true);
                $('#Remarks').attr('readonly', true);
                $(".HideClass").hide();
                $(".ApproveClass").hide();
                $(".CloseClass").hide();
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
    },
    funSubmitUSBRequestFromValue: function () {

        var ChkIsAccept = $('#IsAccept').prop("checked");
        if (ChkIsAccept == true) {
            USBRequestController.funValidateUSBRequestForm();

            if ($("#frmtblUSBRequest").valid()) {
                $('#btnSubmit').attr('disabled', true);
                $.ajax({

                    url: glbParamObject.Virutalpath + '/USBRequest/SubmitUSBRequestFromValue',
                    data: $("#frmtblUSBRequest").serialize(),
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

                                window.location.href = glbParamObject.Virutalpath + '/USBRequest/USBRequest'
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
        }
    },

    funApproveUSBRequestFromValue: function () {
        USBRequestController.funValidateUSBRequestForm();

        if ($("#frmtblUSBRequest").valid()) {
            $.ajax({

                url: glbParamObject.Virutalpath + '/USBRequest/ApproveUSBRequestFromValue',
                data: $("#frmtblUSBRequest").serialize(),
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

                            window.location.href = glbParamObject.Virutalpath + '/USBRequest/USBRequest'
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


    funCloseUSBRequestFromValue: function () {
        USBRequestController.funValidateUSBRequestForm();

        if ($("#frmtblUSBRequest").valid()) {
            $.ajax({

                url: glbParamObject.Virutalpath + '/USBRequest/CloseUSBRequestFromValue',
                data: $("#frmtblUSBRequest").serialize(),
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

                            window.location.href = glbParamObject.Virutalpath + '/USBRequest/USBRequest'
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

    funReturnUSBRequestFromValue: function () {
        $.ajax({

            url: glbParamObject.Virutalpath + '/USBRequest/ReturnUSBRequestFromValue',
            data: $("#frmtblUSBRequest").serialize(),
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

                        window.location.href = glbParamObject.Virutalpath + '/USBRequest/USBRequest'
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

    funRejectUSBRequestFromValue: function () {
        $.ajax({

            url: glbParamObject.Virutalpath + '/USBRequest/RejectUSBRequestFromValue',
            data: $("#frmtblUSBRequest").serialize(),
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

                        window.location.href = glbParamObject.Virutalpath + '/USBRequest/USBRequest'
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
        var USBRequestFormId = $("#USBRequestFormId").val();
        if (USBRequestFormId !== "0") {
            $.ajax({

                url: glbParamObject.Virutalpath + '/USBRequest/getStepCount',
                data: { paramUSBRequestFormId: USBRequestFormId },
                type: 'POST',
                dataType: 'json',
                success: function (responce) {
                    var SuccessCount = responce.length;
                    /* var SuccessCountGreen = parseInt(SuccessCount) - 1;*/
                    //var SuccessResult = responce.leght;
                    var liString = "";



                    liString = "<ul class='progressbar'>";



                    /*liString = liString + "<li class='active'>Request Send For USB</li>";*/
                    for (var i = 1; i < 7; i++) {
                        if (i <= SuccessCount) {
                            var takeiData = i - 1;
                            liString = liString + "<li class='active'>" + responce[takeiData].StatusName + "<br/> Applied by: <br/>" + responce[takeiData].CreateByName + "</li>";
                        }
                        else {
                            /*var thisstp = parseInt(i) + 1;*/
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