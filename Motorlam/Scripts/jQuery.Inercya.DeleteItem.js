(function ($) {
    $.fn.DeleteItem = function (options) {
        var settings = $.extend({}, $.fn.DeleteItem.defaults, options);
        var deleteUrl = $(settings.deleteUrl, this);
        var listContainer = $(settings.listContainer, this);
        var reloadUrl = $(settings.reloadUrl, this);
        var params = $(settings.params, this);

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
                    url: deleteUrl,
                    data: { Id: id },
                    success: function (event) {
                        if (event.result == "success") {                            
                            jQuery("#" + listContainer).load(reloadUrl, params);
                        }
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