var NewItRequisitionController = {

    GetfunDocumentInit: function () {
        if ($("#requestFomsVM_IsPending").val() == 'True') {
            var NewITRequisitionID = $("#NewITRequisitionID").val();

            if (NewITRequisitionID == 0) {
                ;
                $(".clsProcessSteps").hide();
                $(".HideClass").show();
                $(".clsDepartmentHead").hide();
                $(".clsItDepartmentHead").hide();
                $(".ApproveClass").hide();
                $(".clsItDepartmentHead").hide();
                $(".clsHelpdesk").hide();
                $(".clsClose").hide();
                $(".CloseClass").hide();
                $(".clsALLIT").hide();
                $(".DHRemarks").hide();
                $(".ITDHRemark").hide();
                $(".ITHDAdminRemark").hide();
                $(".Readyfordeliverys").hide();
                $(".Delivery").hide();
            }
            else {
                var PendingWith = $("#requestFomsVM_PendingWith").val();
                var Status = $("#requestFomsVM_Status").val();              
                var ReturnRejectWith = $("#requestFomsVM_ReturnRejectWith").val();
                var LastPendingWith = $("#requestFomsVM_LastPendingWith").val();
                if (PendingWith === "" && Status === "Complete") {

                    $('#OnBehalfof').attr('readonly', true);
                    $('#Device').attr("disabled", true);
                    $('#Reason').attr('readonly', true);
                    $('#Location').attr("disabled", true);
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemark').attr('readonly', true);
                    $('#ITHDAdminRemark').attr('readonly', true);

                    $('#AssentID').attr('readonly', true);
                    $('#Mark').attr('readonly', true);
                    $('#Model').attr('readonly', true);
                    $('#Family').attr('readonly', true);
                    $('#OperationSystem').attr('readonly', true);
                    $('#MonitorSize').attr('readonly', true);
                    $('#SerialNO').attr('readonly', true);
                    $('#MACAddress').attr('readonly', true);
                    $('#InstallationRemark').attr('readonly', true);
                    $('#HDD').attr('readonly', true);
                    $('#RAM').attr('readonly', true);
                    $('#DeliveryBY').attr('readonly', true);
                    $('#LocationByDelivery').attr('readonly', true);
                    $('#Area').attr('readonly', true);
                    $('#AntivirusInstalled').attr("disabled", true);
                    $('#SoftwareInstalled').attr("disabled", true);
                    $('#DomainRegistion').attr("disabled", true);
                    $('#OneDriveConfigured').attr("disabled", true);
                    $('#BitlockerConfigured').attr("disabled", true);
                    $('#Device').attr("disabled", true);
                    $("#OTP").hide();
                    $(".HideClass").hide();
                    $(".ApproveClass").hide();
                    $(".Readyfordeliverys").hide();
                    $(".CloseClass").hide();
                    $(".clsOTP").hide();
                }
                if (PendingWith == "Department Head") {                    
                    $('#OnBehalfof').attr('readonly', true);
                    $('#Device').attr("disabled", true);
                    $('#Reason').attr('readonly', true);
                    $('#Location').attr("disabled", true);
                    $(".DHRemarks").show();
                    $(".ITDHRemark").hide();
                    $(".ITHDAdminRemark").hide();
                    $(".clsALLIT").hide();
                    $(".HideClass").hide();
                    $(".ApproveClass").show();
                    $(".CloseClass").hide();
                    $(".Readyfordeliverys").hide();
                    $(".Delivery").hide();
                }
                else if (PendingWith == "IT Department Head") {
                    $('#OnBehalfof').attr('readonly', true);
                    $('#Device').attr("disabled", true);
                    $('#Reason').attr('readonly', true);
                    $('#Location').attr("disabled", true);
                    $('#DHRemarks').attr('readonly', true);

                    $(".ITDHRemark").show();
                    $(".ITHDAdminRemark").hide();
                    $(".clsALLIT").hide();
                    $(".HideClass").hide();
                    $(".ApproveClass").show();
                    $(".CloseClass").hide();
                    $(".Readyfordeliverys").hide();
                    $(".Delivery").hide();
                }
                else if (PendingWith == "IT Helpdesk Admin") {
                    $('#OnBehalfof').attr('readonly', true);
                    $('#Device').attr("disabled", true);
                    $('#Reason').attr('readonly', true);
                    $('#Location').attr("disabled", true);
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemark').attr('readonly', true);
                    $(".ITHDAdminRemark").show();
                    $(".clsALLIT").hide();
                    $(".HideClass").hide();
                    $(".ApproveClass").show();
                    $(".CloseClass").hide();
                    $(".Readyfordeliverys").hide();
                    $(".Delivery").hide();
                }
                else if (PendingWith == "IT Helpdesk" && Status == "Pending IT Helpdesk Approval") {
                    $('#OnBehalfof').attr('readonly', true);
                    $('#Device').attr("disabled", true);
                    $('#Reason').attr('readonly', true);
                    $('#Location').attr("disabled", true);
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemark').attr('readonly', true);
                    $('#ITHDAdminRemark').attr('readonly', true);

                    $(".clsALLIT").show();
                    $(".HideClass").hide();
                    $(".ApproveClass").hide();
                    $(".CloseClass").hide();
                    $(".Readyfordeliverys").show();
                    $(".Delivery").hide();
                }
                else if (PendingWith == "IT Helpdesk" && Status == "Under Processing") {
                    $('#OnBehalfof').attr('readonly', true);
                    $('#Device').attr("disabled", true);
                    $('#Reason').attr('readonly', true);
                    $('#Location').attr("disabled", true);
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemark').attr('readonly', true);
                    $('#ITHDAdminRemark').attr('readonly', true);

                    $(".clsALLIT").show();
                    $(".HideClass").hide();
                    $(".ApproveClass").hide();
                    $(".CloseClass").hide();
                    $(".Readyfordeliverys").show();
                    $(".Delivery").hide();
                }
                else if (PendingWith == "IT Helpdesk Delivery") {
                    $('#OnBehalfof').attr('readonly', true);
                    $('#Device').attr("disabled", true);
                    $('#Reason').attr('readonly', true);
                    $('#Location').attr("disabled", true);
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemark').attr('readonly', true);
                    $('#ITHDAdminRemark').attr('readonly', true);

                    $(".clsALLIT").show();
                    $(".HideClass").hide();
                    $(".ApproveClass").hide();
                    $(".Readyfordeliverys").hide();
                    $(".Delivery").show();
                    $(".CloseClass").show();

                    $("#AssentID").attr("disabled", true);
                    $("#Mark").attr("disabled", true);
                    $("#Model").attr("disabled", true);
                    $("#Family").attr("disabled", true);
                    $("#OperationSystem").attr("disabled", true);
                    $("#MonitorSize").attr("disabled", true);
                    $("#SerialNO").attr("disabled", true);
                    $("#MACAddress").attr("disabled", true);
                    $("#InstallationRemark").attr("disabled", true);
                    $("#HDD").attr("disabled", true);
                    $("#RAM").attr("disabled", true);
                    $('#DeliveryBY').attr('readonly', true);
                    $('#AntivirusInstalled').attr('readonly', true);
                    $('#SoftwareInstalled').attr('readonly', true);
                    $('#DomainRegistion').attr('readonly', true);
                    $('#OneDriveConfigured').attr('readonly', true);
                    $('#BitlockerConfigured').attr('readonly', true);
                }
                else if (PendingWith == "Initiator" && Status == "Returned") {
                    //$(".clsProcessSteps").hide();
                    //$(".HideClass").show();
                    //$(".clsDepartmentHead").hide();
                    //$(".clsItDepartmentHead").hide();
                    //$(".ApproveClass").hide();
                    //$(".clsItDepartmentHead").hide();
                    //$(".clsHelpdesk").hide();
                    //$(".clsClose").hide();
                    //$(".CloseClass").hide();
                    //$(".clsALLIT").hide();
                    //$(".DHRemarks").hide();
                    //$(".ITDHRemark").hide();
                    //$(".ITHDAdminRemark").hide();
                    //$(".Readyfordeliverys").hide();
                    //$(".Delivery").hide();
                    if (ReturnRejectWith == "Department Head") {                                         
                        $(".DHRemarks").show();
                        $(".ITDHRemark").hide();
                        $(".ITHDAdminRemark").hide();
                        $(".clsALLIT").hide();
                        $(".HideClass").show();
                        $(".ApproveClass").hide();
                        $(".CloseClass").hide();
                        $(".Readyfordeliverys").hide();
                        $(".Delivery").hide();
                    }
                    else if (ReturnRejectWith == "IT Department Head") {                       
                    
                        $('#DHRemarks').attr('readonly', true);
                        $(".ITDHRemark").show();
                        $(".ITHDAdminRemark").hide();
                        $(".clsALLIT").hide();
                        $(".HideClass").show();
                        $(".ApproveClass").hide();
                        $(".CloseClass").hide();
                        $(".Readyfordeliverys").hide();
                        $(".Delivery").hide();
                    }
                    else if (ReturnRejectWith == "IT Helpdesk Admin") {                       
                        $('#DHRemarks').attr('readonly', true);
                        $('#ITDHRemark').attr('readonly', true);
                        $(".ITHDAdminRemark").show();
                        $(".clsALLIT").hide();
                        $(".HideClass").show();
                        $(".ApproveClass").hide();
                        $(".CloseClass").hide();
                        $(".Readyfordeliverys").hide();
                        $(".Delivery").hide();
                    }
                    else if (ReturnRejectWith == "IT Helpdesk" && LastPendingWith == "Pending IT Helpdesk Approval") {                       
                        $('#DHRemarks').attr('readonly', true);
                        $('#ITDHRemark').attr('readonly', true);
                        $('#ITHDAdminRemark').attr('readonly', true);

                        $(".clsALLIT").show();
                        $(".HideClass").show();
                        $(".ApproveClass").hide();
                        $(".CloseClass").hide();
                        $(".Readyfordeliverys").hide();
                        $(".Delivery").hide();
                    }
                    else if (ReturnRejectWith == "IT Helpdesk" && LastPendingWith == "Under Processing") {                     
                        $('#DHRemarks').attr('readonly', true);
                        $('#ITDHRemark').attr('readonly', true);
                        $('#ITHDAdminRemark').attr('readonly', true);

                        $(".clsALLIT").show();
                        $(".HideClass").show();
                        $(".ApproveClass").hide();
                        $(".CloseClass").hide();
                        $(".Readyfordeliverys").hide();
                        $(".Delivery").hide();
                    }
                    else if (ReturnRejectWith == "IT Helpdesk Delivery") {
                        $('#OnBehalfof').attr('readonly', true);
                        $('#Device').attr("disabled", true);
                        $('#Reason').attr('readonly', true);
                        $('#Location').attr("disabled", true);
                        $('#DHRemarks').attr('readonly', true);
                        $('#ITDHRemark').attr('readonly', true);
                        $('#ITHDAdminRemark').attr('readonly', true);

                        $(".clsALLIT").show();
                        $(".HideClass").show();
                        $(".ApproveClass").hide();
                        $(".Readyfordeliverys").hide();
                        $(".Delivery").show();
                        $(".CloseClass").show();

                        $("#AssentID").attr("disabled", true);
                        $("#Mark").attr("disabled", true);
                        $("#Model").attr("disabled", true);
                        $("#Family").attr("disabled", true);
                        $("#OperationSystem").attr("disabled", true);
                        $("#MonitorSize").attr("disabled", true);
                        $("#SerialNO").attr("disabled", true);
                        $("#MACAddress").attr("disabled", true);
                        $("#InstallationRemark").attr("disabled", true);
                        $("#HDD").attr("disabled", true);
                        $("#RAM").attr("disabled", true);
                        $('#DeliveryBY').attr('readonly', true);
                        $('#AntivirusInstalled').attr('readonly', true);
                        $('#SoftwareInstalled').attr('readonly', true);
                        $('#DomainRegistion').attr('readonly', true);
                        $('#OneDriveConfigured').attr('readonly', true);
                        $('#BitlockerConfigured').attr('readonly', true);
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
            var NewITRequisitionID = $("#NewITRequisitionID").val();
            var PendingWith = $("#requestFomsVM_PendingWith").val();
            var Status = $("#requestFomsVM_Status").val();
            var ReturnRejectWith = $("#requestFomsVM_ReturnRejectWith").val();
            var LastPendingWith = $("#requestFomsVM_LastPendingWith").val();
            if (NewITRequisitionID == 0) {

                $(".clsProcessSteps").hide();
                $(".HideClass").show();
                $(".clsDepartmentHead").hide();
                $(".clsItDepartmentHead").hide();
                $(".ApproveClass").hide();

                $(".clsHelpdesk").hide();
                $(".clsClose").hide();
                $(".CloseClass").hide();
                $(".clsALLIT").hide();
                $(".DHRemarks").hide();
                $(".ITDHRemark").hide();
                $(".ITHDAdminRemark").hide();
                $(".Readyfordeliverys").hide();
                $(".Delivery").hide();
            }
            else if (PendingWith == "Initiator" && Status == "Returned") {               
                if (ReturnRejectWith == "Department Head") {
                    $(".DHRemarks").show();
                    $(".ITDHRemark").hide();
                    $(".ITHDAdminRemark").hide();
                    $(".clsALLIT").hide();
                    $(".HideClass").show();
                    $(".ApproveClass").hide();
                    $(".CloseClass").hide();
                    $(".Readyfordeliverys").hide();
                    $(".Delivery").hide();
                }
                else if (ReturnRejectWith == "IT Department Head") {

                    $('#DHRemarks').attr('readonly', true);
                    $(".ITDHRemark").show();
                    $(".ITHDAdminRemark").hide();
                    $(".clsALLIT").hide();
                    $(".HideClass").show();
                    $(".ApproveClass").hide();
                    $(".CloseClass").hide();
                    $(".Readyfordeliverys").hide();
                    $(".Delivery").hide();
                }
                else if (ReturnRejectWith == "IT Helpdesk Admin") {
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemark').attr('readonly', true);
                    $(".ITHDAdminRemark").show();
                    $(".clsALLIT").hide();
                    $(".HideClass").show();
                    $(".ApproveClass").hide();
                    $(".CloseClass").hide();
                    $(".Readyfordeliverys").hide();
                    $(".Delivery").hide();
                }
                else if (ReturnRejectWith == "IT Helpdesk" && LastPendingWith == "Pending IT Helpdesk Approval") {
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemark').attr('readonly', true);
                    $('#ITHDAdminRemark').attr('readonly', true);

                    $(".clsALLIT").show();
                    $(".HideClass").show();
                    $(".ApproveClass").hide();
                    $(".CloseClass").hide();
                    $(".Readyfordeliverys").hide();
                    $(".Delivery").hide();
                }
                else if (ReturnRejectWith == "IT Helpdesk" && LastPendingWith == "Under Processing") {
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemark').attr('readonly', true);
                    $('#ITHDAdminRemark').attr('readonly', true);

                    $(".clsALLIT").show();
                    $(".HideClass").show();
                    $(".ApproveClass").hide();
                    $(".CloseClass").hide();
                    $(".Readyfordeliverys").hide();
                    $(".Delivery").hide();
                }
                else if (ReturnRejectWith == "IT Helpdesk Delivery") {
                    $('#OnBehalfof').attr('readonly', true);
                    $('#Device').attr("disabled", true);
                    $('#Reason').attr('readonly', true);
                    $('#Location').attr("disabled", true);
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemark').attr('readonly', true);
                    $('#ITHDAdminRemark').attr('readonly', true);

                    $(".clsALLIT").show();
                    $(".HideClass").show();
                    $(".ApproveClass").hide();
                    $(".Readyfordeliverys").hide();
                    $(".Delivery").show();
                    $(".CloseClass").show();

                    $("#AssentID").attr("disabled", true);
                    $("#Mark").attr("disabled", true);
                    $("#Model").attr("disabled", true);
                    $("#Family").attr("disabled", true);
                    $("#OperationSystem").attr("disabled", true);
                    $("#MonitorSize").attr("disabled", true);
                    $("#SerialNO").attr("disabled", true);
                    $("#MACAddress").attr("disabled", true);
                    $("#InstallationRemark").attr("disabled", true);
                    $("#HDD").attr("disabled", true);
                    $("#RAM").attr("disabled", true);
                    $('#DeliveryBY').attr('readonly', true);
                    $('#AntivirusInstalled').attr('readonly', true);
                    $('#SoftwareInstalled').attr('readonly', true);
                    $('#DomainRegistion').attr('readonly', true);
                    $('#OneDriveConfigured').attr('readonly', true);
                    $('#BitlockerConfigured').attr('readonly', true);
                }
            }
            else if (Status == "Reject") {
                if (ReturnRejectWith == "Department Head") {
                    $(".DHRemarks").show();
                    $(".ITDHRemark").hide();
                    $(".ITHDAdminRemark").hide();
                    $(".clsALLIT").hide();
                    $(".HideClass").hide();
                    $(".ApproveClass").hide();
                    $(".CloseClass").hide();
                    $(".Readyfordeliverys").hide();
                    $(".Delivery").hide();
                }
                else if (ReturnRejectWith == "IT Department Head") {

                    $('#DHRemarks').attr('readonly', true);
                    $(".ITDHRemark").show();
                    $(".ITHDAdminRemark").hide();
                    $(".clsALLIT").hide();
                    $(".HideClass").hide();
                    $(".ApproveClass").hide();
                    $(".CloseClass").hide();
                    $(".Readyfordeliverys").hide();
                    $(".Delivery").hide();
                }
                else if (ReturnRejectWith == "IT Helpdesk Admin") {
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemark').attr('readonly', true);
                    $(".ITHDAdminRemark").show();
                    $(".clsALLIT").hide();
                    $(".HideClass").hide();
                    $(".ApproveClass").hide();
                    $(".CloseClass").hide();
                    $(".Readyfordeliverys").hide();
                    $(".Delivery").hide();
                }
                else if (ReturnRejectWith == "IT Helpdesk" && LastPendingWith == "Pending IT Helpdesk Approval") {
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemark').attr('readonly', true);
                    $('#ITHDAdminRemark').attr('readonly', true);

                    $(".clsALLIT").show();
                    $(".HideClass").hide();
                    $(".ApproveClass").hide();
                    $(".CloseClass").hide();
                    $(".Readyfordeliverys").hide();
                    $(".Delivery").hide();
                }
                else if (ReturnRejectWith == "IT Helpdesk" && LastPendingWith == "Under Processing") {
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemark').attr('readonly', true);
                    $('#ITHDAdminRemark').attr('readonly', true);

                    $(".clsALLIT").show();
                    $(".HideClass").hide();
                    $(".ApproveClass").hide();
                    $(".CloseClass").hide();
                    $(".Readyfordeliverys").hide();
                    $(".Delivery").hide();
                }
                else if (ReturnRejectWith == "IT Helpdesk Delivery") {
                    $('#OnBehalfof').attr('readonly', true);
                    $('#Device').attr("disabled", true);
                    $('#Reason').attr('readonly', true);
                    $('#Location').attr("disabled", true);
                    $('#DHRemarks').attr('readonly', true);
                    $('#ITDHRemark').attr('readonly', true);
                    $('#ITHDAdminRemark').attr('readonly', true);

                    $(".clsALLIT").show();
                    $(".HideClass").hide();
                    $(".ApproveClass").hide();
                    $(".Readyfordeliverys").hide();
                    $(".Delivery").show();
                    $(".CloseClass").show();

                    $("#AssentID").attr("disabled", true);
                    $("#Mark").attr("disabled", true);
                    $("#Model").attr("disabled", true);
                    $("#Family").attr("disabled", true);
                    $("#OperationSystem").attr("disabled", true);
                    $("#MonitorSize").attr("disabled", true);
                    $("#SerialNO").attr("disabled", true);
                    $("#MACAddress").attr("disabled", true);
                    $("#InstallationRemark").attr("disabled", true);
                    $("#HDD").attr("disabled", true);
                    $("#RAM").attr("disabled", true);
                    $('#DeliveryBY').attr('readonly', true);
                    $('#AntivirusInstalled').attr('readonly', true);
                    $('#SoftwareInstalled').attr('readonly', true);
                    $('#DomainRegistion').attr('readonly', true);
                    $('#OneDriveConfigured').attr('readonly', true);
                    $('#BitlockerConfigured').attr('readonly', true);
                }
            }
            else {
                $(".clsProcessSteps").hide();
                $(".HideClass").hide();
                $(".clsDepartmentHead").hide();
                $(".clsItDepartmentHead").hide();
                $(".ApproveClass").hide();

                $(".clsHelpdesk").hide();
                $(".clsClose").hide();
                $(".CloseClass").hide();
                /* $(".clsALLIT").hide();*/
                /* $(".DHRemarks").hide();*/
                /* $(".ITDHRemark").hide();*/
                /*  $(".ITHDAdminRemark").hide();*/
                $(".Readyfordeliverys").hide();
                /*$(".Delivery").hide();*/

                $("#OnBehalfof").attr('readonly', true);
                $('#Location').attr("disabled", true);

                $("#Device").attr('readonly', true);
                $("#Reason").attr('readonly', true);
                $("#DHRemarks").attr('readonly', true);
                $("#ITDHRemark").attr('readonly', true);
                $("#ITHDAdminRemark").attr('readonly', true);
                $("#AssentID").attr('readonly', true);
                $("#Mark").attr('readonly', true);
                $("#Model").attr('readonly', true);
                $("#Family").attr('readonly', true);
                $("#OperationSystem").attr('readonly', true);
                $("#MonitorSize").attr('readonly', true);
                $("#SerialNO").attr('readonly', true);
                $("#MACAddress").attr('readonly', true);
                $("#InstallationRemark").attr('readonly', true);
                $("#HDD").attr('readonly', true);
                $("#RAM").attr('readonly', true);
                $("#DeliveryBY").attr('readonly', true);

                $("#AntivirusInstalled").attr("disabled", true);

                $("#SoftwareInstalled").attr("disabled", true);

                $("#DomainRegistion").attr("disabled", true);

                $("#OneDriveConfigured").attr("disabled", true);

                $("#BitlockerConfigured").attr("disabled", true);
                $("#LocationByDelivery").attr("disabled", true);

                $("#Area").attr('readonly', true);
                $(".clsOTP").hide();
            }
        }
    },

    funValidateNewItForm: function () {

        $("#frmtblNewIt").validate({
            // Rules for form validation
            onkeyup: false,
            rules: {
                Location: {
                    required: true,
                },
                Device: {
                    required: true,
                },
            },
            // Messages for form validation
            messages: {
                Location: {
                    required: 'Please select location',
                },
                Device: {
                    required: 'Please select device',
                },
            },
            // Do not change code below
            errorPlacement: function (error, element) {
                error.insertAfter(element.parent());
            }
        });
    },

    funValidateNewItAssentForm: function () {
        $("#frmtblNewIt").validate({
            // Rules for form validation
            onkeyup: false,
            rules: {
                AssentID: {
                    required: true,
                },
            },
            // Messages for form validation
            messages: {
                AssentID: {
                    required: 'Please enter AssetID',
                },
            },
            // Do not change code below
            errorPlacement: function (error, element) {
                error.insertAfter(element.parent());
            }
        });
    },

    funValidateNewItCloseForm: function () {
        $("#frmtblNewIt").validate({
            // Rules for form validation
            onkeyup: false,
            rules: {
                AssentID: {
                    required: true,
                },
            },
            // Messages for form validation
            messages: {
                AssentID: {
                    required: 'Please enter AssetID',
                },
            },
            // Do not change code below
            errorPlacement: function (error, element) {
                error.insertAfter(element.parent());
            }
        });
    },

    funSubmitNewItFromValue: function () {

        NewItRequisitionController.funValidateNewItForm();

        if ($("#frmtblNewIt").valid()) {
            $('#btnSubmit').attr('disabled', true);
            $.ajax({
                url: glbParamObject.Virutalpath + '/NewItRequisition/SubmitNewItRequisitionFromValue',
                data: $("#frmtblNewIt").serialize(),
                type: 'POST',
                dataType: 'json',
                success: function (responce) {
                    debugger;
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

                            window.location.href = glbParamObject.Virutalpath + '/NewItRequisition/NewItRequisitionForms'
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

    funApproveNewItFromValue: function () {
        NewItRequisitionController.funValidateNewItForm();

        if ($("#frmtblNewIt").valid()) {
            $.ajax({

                url: glbParamObject.Virutalpath + '/NewItRequisition/ApproveNewItRequisitionFromValue',
                data: $("#frmtblNewIt").serialize(),
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

    funUnderProcessFromValue: function () {
        NewItRequisitionController.funValidateNewItForm();

        if ($("#frmtblNewIt").valid()) {
            $.ajax({

                url: glbParamObject.Virutalpath + '/NewItRequisition/UnderProcessFromValue',
                data: $("#frmtblNewIt").serialize(),
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

    funReadyForDelivery: function () {
        NewItRequisitionController.funValidateNewItForm();
        NewItRequisitionController.funValidateNewItAssentForm();
        if ($("#frmtblNewIt").valid()) {
            $.ajax({

                url: glbParamObject.Virutalpath + '/NewItRequisition/ApproveNewItRequisitionFromValue',
                data: $("#frmtblNewIt").serialize(),
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

    funCloseNewItFromValue: function () {

        NewItRequisitionController.funValidateNewItCloseForm();

        if ($("#frmtblNewIt").valid()) {
            $.ajax({

                url: glbParamObject.Virutalpath + '/NewItRequisition/CompleteNewITForm',
                data: $("#frmtblNewIt").serialize(),
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

                            window.location.href = glbParamObject.Virutalpath + '/NewItRequisition/NewItRequisitionForms'
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

    funReturnNewItFromValue: function () {
        $.ajax({

            url: glbParamObject.Virutalpath + '/NewItRequisition/ReturnNewItFromValue',
            data: $("#frmtblNewIt").serialize(),
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

                        window.location.href = glbParamObject.Virutalpath + '/NewItRequisition/NewItRequisitionForms'
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

    funRejectNewItFromValue: function () {
        $.ajax({

            url: glbParamObject.Virutalpath + '/NewItRequisition/RejectNewItFromValue',
            data: $("#frmtblNewIt").serialize(),
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

                        window.location.href = glbParamObject.Virutalpath + '/NewItRequisition/NewItRequisitionForms'
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

    funGetDeliveryByData: function () {
        ;
        $.ajax({
            url: glbParamObject.Virutalpath + '/NewItRequisition/GetDeliveryByData',
            type: 'get',
            dataType: 'json',
            success: function (responce) {
                if (responce.Result == "OK") {
                    $("#DeliveryBY").empty();
                    $("#DeliveryBY").append($("<option></option>").val("").html("Select DeliveryBY"));
                    $.map(responce.OtherParameter, function (item) {
                        ;
                        console.log(item);
                        $("#DeliveryBY").append($("<option></option>").val(item.PsNo).html(item.Name));
                    });
                    var currentDelivryBy = $("#hdnDeliveryBy").val();
                    $("#DeliveryBY").val(currentDelivryBy);
                }
            },
            error: function (xxx) {

            }
        });
    },
}