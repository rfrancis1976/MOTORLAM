(function ($) {
    $.fn.Search = function (options) {
        var settings = $.extend({}, $.fn.Search.defaults, options);

        this.click(function (e) {
            e.preventDefault();
            jQuery("#" + settings.formName).ajaxSubmit({
                target: "#" + settings.listContainer,
                data: settings.params
            });
        });
    };
    $.fn.Search.defaults = {
        formName: "none",
        listContainer: "ListContainer",
        params: null
    };
})(jQuery);