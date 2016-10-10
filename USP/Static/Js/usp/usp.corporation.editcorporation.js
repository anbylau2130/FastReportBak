(function () {
    usp.namespace("usp.corporation.editCorporation");
    usp.corporation.editCorporation = {
        init: function (btnReturnId, provinceId, areaId, areaUrl, countyId, countyUrl, basePage) {
            $(".datepicker").datetimepicker({
                format: 'YYYY-MM-DD'
            });

            usp.corporation.editCorporation.eventInit.btnReturnInit(btnReturnId, basePage);
            //usp.corporation.editCorporation.eventInit.provinceChange(provinceId, provinceUrl);
            usp.corporation.editCorporation.eventInit.provinceChange(areaId, areaUrl, provinceId, countyId);
            usp.corporation.editCorporation.eventInit.areaChange(countyId, countyUrl, areaId);
           
        },
        eventInit: {
            btnReturnInit: function (id, basePage) {
                $(id).on("click", function () {
                    location.href = basePage;
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
                            $.each(data, function (i, item) {
                                $("<option></option>")
                                    .val(item.id)
                                    .text(item.text)
                                    .appendTo($(areaId));
                            });
                            $(countyId).empty();
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
                            $.each(data, function (i, item) {
                                $("<option></option>")
                                    .val(item.id)
                                    .text(item.text)
                                    .appendTo($(countyId));
                            });
                        }
                    });
                });

            }
        },
        initFileUpload:function (id, uploadUrl, filename,hiddenid) {
        $(id).fileinput({
            language: 'zh', //设置语言
            uploadUrl: uploadUrl, //上传的地址
            allowedFileExtensions: ['jpg', 'png', 'gif'], //接收的文件后缀
            showUpload: false, //是否显示上传按钮
            showCaption: false, //是否显示标题
            browseClass: "btn btn-primary", //按钮样式
            previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
            initialPreview: [ //预览图片的设置
                filename!=''? "<img src='"+filename+"' class='file-preview-image' alt='Logo' title='Logo'>":""
            ]
        });
        $(id).on("fileuploaded", function (event, data, previewId, index) {
            if (data.jqXHR.responseJSON.IsSuccess) {
                $(hiddenid).val(data.jqXHR.responseJSON.FileName);
            } else {
                toastr.error(data.jqXHR.responseJSON.Msg);
            }
        });
    }
    }
})(this);