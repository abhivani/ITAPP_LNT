var RoleMasterController = {
    funValidateRoleMasterForm: function () {      

        $("#frmtblRoleMaster").validate({
            // Rules for form validation
            onkeyup: false,
            rules: {
                PsNo: {
                    required: true,
                    //|Data Already exist Kalpesh End|
                },
                Name: {
                    required: true,
                }, 
                Role: {
                    required: true,
                }, 

            },
            // Messages for form validation
            messages: {
                PsNo: {
                    required: 'Please enter PS No',
                },

                Name: {
                    required: 'Please enter Name',
                },
                Role: {
                    required: 'Please enter Role',
                },
            },
            // Do not change code below
            errorPlacement: function (error, element) {
                error.insertAfter(element.parent());
            }
        });
    },
    funSaveRoleMasterValue: function () {      
        //$("#btnSubmit").ladda().ladda('start');
        //Start: For Dropdown validation
        RoleMasterController.funValidateRoleMasterForm();

        if ($("#frmtblRoleMaster").valid()) {

            $.ajax({

                url: glbParamObject.Virutalpath + '/RoleMaster/SaveRoleMasterValue',
                data: $("#frmtblRoleMaster").serialize(),
                type: 'POST',
                dataType: 'json',
                success: function (responce) {                  
                    $("#divAlert").html("");
                    if (responce.Result == "OK") {
                        toastr.success(responce.Message);
                        window.setTimeout(function () {
                            window.location.href = glbParamObject.Virutalpath + '/RoleMaster/Index'
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

                            window.location.href = glbParamObject.Virutalpath + '/RoleMaster/Index'
                        }, 5000);
                    }
                    else if (responce.Result == "ERROR") {
                        $("#divAlert").html(responce.OtherParameter.AlertMessage);
                        $("#divAlert").show();
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

    funDeleteThisRecord: function (_paramPkId) {

        if (_paramPkId > 0) {




            $.confirm({
                text: "Are you sure you want to delete this record ?",
                title: "Confirmation",
                confirmButton: "Yes",
                cancelButton: "No",
                post: false,
                confirmButtonClass: "btn btn-success",
                cancelButtonClass: "btn btn-danger",
                dialogClass: "modal-dialog ",
                confirm: function (button) {

                    $("body").removeClass('modal-open');
                    var id = parseInt(_paramPkId);


                    $.ajax({
                        type: "GET",
                        /*url: '@Url.Action("deletRecord", "RoleMaster")',*/
                        url: glbParamObject.Virutalpath + '/RoleMaster/deletRecord',
                        contentType: "application/json; charset=utf-8",
                        data: { paramPkId: _paramPkId },
                        dataType: "json",
                        success: function (responce) {

                            if (responce.Result == "OK") {
                                toastr.success(responce.Message);

                                window.setTimeout(function () {
                                    window.location.href = glbParamObject.Virutalpath + '/RoleMaster/Index'
                                }, 1000);


                            }
                            else if (responce.Result == "WARNING") {
                                toastr.warning(responce.Message);
                            }
                            else if (responce.Result == "ERROR") {
                                toastr.error(responce.Message);

                            }

                        }

                    });


                },
                cancel: function (button) {

                },
            });
        }
        else {
            toastr.warning("No record found.");
            return;
        }
    },

    funOpenAddOrEditForm: function (paramRoleId) {
        debugger;
         
        window.location.href = glbParamObject.Virutalpath + '/RoleMaster/AddEditRoleMastersData/' + paramRoleId


    },  

    GetfunAutocomInit: function () {        
        $("#PsNo").autocomplete({
            source: function (request, response) {
                if ($("#PsNo").val() != '') {
                    $.ajax({                      
                        url: glbParamObject.Virutalpath + '/RoleMaster/GetRoleMasterByPsNo',
                        dataType: "json",
                        type: "POST",
                        delay: 250,
                        async: false,
                        data: {
                            //__RequestVerificationToken: gettoken(),
                            //paramPsNo: extractLast(request.term)
                            paramPsNo: request.term

                        },
                        success: function (res) {                            
                            console.log(res);

                            response($.map(res.OtherParameter.PsnoData, function (item) {                              
                                return {
                                    label: item.Name,
                                    val: item.Name
                                };
                            }));


                        }

                    });
                }
            },
            minLength: 1,
            autoFocus: true,
            change: function (event, ui) {
                //if (ui.item) {
                   
                //    return false;
                //}
                //else {
                    var SelectedPsNO = $("#PsNo").val();
                    if (SelectedPsNO != "") {
                        $.ajax({
                            // url: '/RoleMaster/GetRoleMasterByPsNo',
                          //  url: "@Url.Action("GetRoleMasterNameByPsNo", "RoleMaster")",
                            url: glbParamObject.Virutalpath + '/RoleMaster/GetRoleMasterNameByPsNo',
                            dataType: "json",
                            type: "POST",
                            delay: 250,
                            async: false,
                            data: {
                                //paramPsNo: extractLast(SelectedPsNO)
                                paramPsNo: SelectedPsNO
                            },
                            success: function (res) {
                                console.log(res);
                                if (res.Result != "OK") {
                                    $("#Name").val("");
                                    $("#PsNo").val("");
                                    toastr.error(res.Message);
                                }
                                else {
                                    $("#Name").val(res.OtherParameter.PsnoData.t_name);
                                }
                            }

                        });

                    }
                    else {
                        debugger;
                        $("#Name").val("");
                    }
                //}

                //if (ui.item === null) {
                //    toastr.warning('Select Psno from Suggestion');
                //    $(this).val('');
                //}
            },
            focus: function () {

                return false;
            },
            select: function (event, ui) {
                debugger;
                var SelectedPsNO = ui.item.val;
                if (SelectedPsNO != "") {
                    $.ajax({
                        // url: '/RoleMaster/GetRoleMasterByPsNo',
                        //url: "@Url.Action("GetRoleMasterNameByPsNo", "RoleMaster")",
                        url: glbParamObject.Virutalpath + '/RoleMaster/GetRoleMasterNameByPsNo',
                        dataType: "json",
                        type: "POST",
                        delay: 250,
                        async: false,
                        data: {
                            //paramPsNo: extractLast(SelectedPsNO)
                            paramPsNo: SelectedPsNO
                        },
                        success: function (res) {

                            console.log(res);
                            if (res == false) {
                                $("#Name").val("");
                            }
                            else {
                                $("#Name").val(res.t_name);
                            }
                        }

                    });

                }
                else {
                    debugger;
                    $("#Name").val("");
                }
            },
        }).on("keydown", function (event) {

            if (event.keyCode === $.ui.keyCode.TAB &&
                $(this).autocomplete("instance").menu.active) {
                event.preventDefault();
            }
        });
    },
}