(function ($) {
    $.fn.ReloadCascadeDropDown = function (options) {

        var settings = $.extend({}, $.fn.ReloadCascadeDropDown.defaults, options);

        this.change(function () {
            if (jQuery("#" + settings.fatherFieldName).val() != null && jQuery("#" + settings.fatherFieldName).val() != '') {
                jQuery.ajax({
                    type: "GET",
                    url: settings.loadChildUrl,
                    data: { fatherId: jQuery("#" + settings.fatherFieldName).val() },
                    success: function (response) {
                        
                        document.getElementById(settings.childFieldName).options.length = 0;
                        document.getElementById(settings.childFieldName).add(new Option("<< seleccione >>", ""))

                        if (response.ChildItems.length > 0) {
                            for (var item in response.ChildItems) {
                                jQuery("#" + settings.childFieldName).append("<option value='" + response.ChildItems[item].value + "'>" + response.ChildItems[item].text + "</option>");
                            }
                        }
                    },
                    dataType: "json",
                    cache: false
                });
            }
            else {
                document.getElementById(settings.childFieldName).options.length = 0;
                document.getElementById(settings.childFieldName).add(new Option("<< seleccione >>", ""))
            }
        });
    };
    $.fn.ReloadCascadeDropDown.defaults = {
        fatherFieldName: "none",
        childFieldName: "none",
        loadChildUrl: "none"
    };
})(jQuery);