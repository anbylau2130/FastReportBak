(function () {
    usp.namespace("usp.corporation.auditorCorporation");
    usp.corporation.auditorCorporation = {
        init: function (btnReturnId, provinceId, areaId, areaUrl, countyId, countyUrl) {
            $(".datepicker").datetimepicker({
                format: 'YYYY-MM-DD'
            });

            usp.corporation.auditorCorporation.eventInit.btnReturnInit(btnReturnId);
            //usp.corporation.auditorcorporation.eventInit.provinceChange(provinceId, provinceUrl);
            usp.corporation.auditorCorporation.eventInit.provinceChange(areaId, areaUrl, provinceId, countyId);
            usp.corporation.auditorCorporation.eventInit.areaChange(countyId, countyUrl, areaId);
        },
        eventInit: {
            btnReturnInit: function (id) {
                $(id).on("click", function () {
                    window.history.back();
                    return false;
                });
            },
            provinceChange: function (areaId, url, provinceId, countyId) {
                $(provinceId).on("change", function () {
                    $.ajax({
                        type: "POST",
                        url: url + "?provinceCode=" + $(provinceId).val(),
                        success: function (data) {
                            $(areaId).empty();
                            $(areaId).select2({
                                placeholder: "请选择市区",
                                data: data
                            });
                            $(countyId).empty();
                            $(countyId).select2({
                                placeholder: "请选择县或地区"
                            });
                        }
                    });
                });
            },
            areaChange: function (countyId, url, cityId) {
                $(cityId).on("change", function () {
                    $.ajax({
                        type: "POST",
                        url: url + "?areaCode=" + $(cityId).val(),
                        success: function (data) {
                            $(countyId).empty();
                            $(countyId).select2({
                                placeholder: "请选择县或地区",
                                data: data
                            });
                        }
                    });
                });

            }
        }
    }
})(this);