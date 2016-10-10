(function () {
    Date.prototype.format = function (format) //author: meizz 
    {
        var o = {
            'M+': this.getMonth() + 1, //month 
            'd+': this.getDate(), //day 
            'h+': this.getHours(), //hour 
            'm+': this.getMinutes(), //minute 
            's+': this.getSeconds(), //second 
            'q+': Math.floor((this.getMonth() + 3) / 3), //quarter 
            'S': this.getMilliseconds() //millisecond 
        }
        if (/(y+)/.test(format)) format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        for (var k in o)
            if (new RegExp('(' + k + ')').test(format))
                format = format.replace(RegExp.$1,
                    RegExp.$1.length == 1 ? o[k] :
                    ('00' + o[k]).substr(('' + o[k]).length));
        return format;
    };
    usp = window.usp || (window.usp = {});
    usp.namespace = function () {
        var a = arguments,
            o = null,
            i, j, d, rt;
        for (i = 0; i < a.length; ++i) {
            d = a[i].split('.');
            rt = d[0];
            eval('if (typeof ' + rt + ' == "undefined"){' + rt + ' = {};} o = ' + rt + ';');
            for (j = 1; j < d.length; ++j) {
                o[d[j]] = o[d[j]] || {};
                o = o[d[j]];
            }
        }
    };
    usp.ParseUTCDate = function (milliseconds) {
        //alert(milliseconds);
        if (milliseconds != null) {
            var datetime = milliseconds.replace(new RegExp(/\//g), '');
            eval('var datetime = new ' + datetime);
            return datetime.format('yyyy年MM月dd日 hh:mm:ss');
        } else {
            return "";
        }
    };
    usp.ParseShortDate = function (milliseconds) {
        //alert(milliseconds);
        if (milliseconds != null) {
            var datetime = milliseconds.replace(new RegExp(/\//g), '');
            eval('var datetime = new ' + datetime);
            return datetime.format('yyyy年MM月dd日');
        } else {
            return "";
        }
    };

    usp.ParseUTCDateToDate = function (milliseconds) {
        //alert(milliseconds);
        if (milliseconds != null) {
            var temp = milliseconds.replace(new RegExp(/\//g), '');
            eval('var temp = new ' + temp);
            return temp;
        } else {
            return '';
        }
    };
    //获取某一月的第一天
    usp.GetMonthFirstDay = function (iYear, iMonth) {
        iMonth = iMonth - 1;
        return (new Date(iYear, iMonth, 1)).format("yyyy-MM-dd");
    }
    //获取某一月的最后一天
    usp.GetMonthLastDay = function (iYear, iMonth) {
        iMonth = iMonth - 1;

        var MonthNextFirstDay = new Date(iYear, parseInt(iMonth) + 1, 1);
        return (new Date(MonthNextFirstDay - 86400000)).format("yyyy-MM-dd");
    }

    //获取某一季度的第一天
    usp.GetQuarterFirstDay = function (iYear, iQuarter) {
        return getMonthFirstDay(iYear, (parseInt(iQuarter) - 1) * 3);
    }
    //获取某一季度的最后一天
    usp.GetQuarterLastDay = function (iYear, iQuarter) {
        return getMonthLastDay(iYear, (parseInt(iQuarter) - 1) * 3 + 2);
    }
    //获取某一年的第一天
    usp.GetYearFirstDay = function (iYear) {
        return (new Date(iYear, 0, 1)).format("yyyy-MM-dd");
    }
    //获取某一年的最后一天
    usp.GetYearLastDay = function (iYear) {
        var YearNextFirstDay = new Date(parseInt(iYear) + 1, 0, 1);
        return (new Date(YearNextFirstDay - 86400000)).format("yyyy-MM-dd");
    }
    usp.resizeIframe = function (obj) {
        //alert(obj.contentWindow.document.body.scrollHeight);
        //obj.style.height = obj.contentWindow.document.body.scrollHeight + 'px';
    }

    usp.addTab = function (tab_container, tab_icon, tab_title, tab_href) {
        var container = tab_container || usp.tabContainer;
        if (container.tabs('exists', tab_title)) {
            container.tabs('select', tab_title);
        } else {
            if (tab_href) {
                var content = '<iframe scrolling="auto" frameborder="0"  src="' + tab_href + '" style="overflow:hidden;height:100%;width:100%" height="100%" width="100%" onload="javascript:usp.resizeIframe(this);"></iframe>';
            } else {
                content = '';
            }
            if (tab_icon) {
                container.tabs('add', {
                    icon: tab_icon,
                    title: tab_title,
                    closable: true,
                    content: content
                    //width: container.parent().width(),
                    //height: container.parent().height()
                });
            } else {
                container.tabs('add', {
                    title: tab_title,
                    closable: true,
                    content: content
                    //width: container.parent().width(),
                    //height: container.parent().height()
                });
            }
            container.tabs('select', tab_title);
        }
    };

    usp.init = function () {
        if (typeof (toastr) == 'object') {
            toastr.options.positionClass = 'toast-top-center';
            toastr.options.timeOut = '2000';
            toastr.options.extendedTimeOut = '1000';
        }
    };

    

})(this)