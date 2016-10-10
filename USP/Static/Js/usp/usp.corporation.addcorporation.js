(function () {
    usp.namespace("usp.corporatin.addCorporation");

    usp.corporatin.addCorporation = {
        init : function (btnReturnId,basePage) {
            usp.corporatin.addCorporation.eventInit.btnReturnInit(btnReturnId, basePage);
        },
        eventInit: {
            btnReturnInit: function (id,basePage) {
                $(id).on("click", function () {
                    location.href = basePage;
                    return false;
                });
            }
        }
    }
})(this);