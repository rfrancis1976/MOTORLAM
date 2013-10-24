(function ($) {
    $.fn.DeleteItem = function (options) {
        var settings = $.extend({}, $.fn.DeleteItem.defaults, options);

        this.click(function (e) {
            e.preventDefault();
            var id = this.id.split('_')[1];
            var buttons = {};
            buttons["Cancelar"] = function () {
                jQuery(this).dialog('close');
            }
            buttons["Aceptar"] = function () {
                jQuery(this).dialog('close');
                $.ajax({
                    type: "POST",
                    url: settings.deleteUrl,
                    data: { Id: id },
                    success: function (event) {
                        if (event.result == "success") {
                            jQuery("#" + settings.listContainer).load(settings.reloadUrl, settings.params);
                            jQuery("#General_DeleteCorrect").show();
                        } else {
                            if (event.errorMessage != undefined) {
                                alert(response.errorMessage);
                            }
                            else {
                                jQuery("#General_Notify").show();
                                jQuery("#General_Success").hide();
                                jQuery("#General_Notify .validationClose").remove();
                                if (event.validationErrors) {
                                    jQuery("#General_Notify ul").fromTemplate('templateErrors', event);
                                    ShowValidationErrors("#" + settings.formName, event.validationErrors, "errorListTemplate");
                                }
                            }
                        }
                        jQuery(".validationClose").click(function (e) {
                            e.preventDefault();
                            jQuery(".validationDiv, .validationDivError").hide("slow");

                        });
                    },
                    dataType: "json",
                    cache: false
                });
            }
            jQuery("#DeleteDialog").dialog({
                autoOpen: true,
                modal: true,
                show: 'blind',
                hide: 'blind',
                width: 350,
                buttons: buttons
            });
        });
    };
    $.fn.DeleteItem.defaults = {
        deleteUrl: "none",
        listContainer: "ListContainer",
        reloadUrl: "none",
        params: null
    };
})(jQuery);