/* 
* jQuery fromTemplate Plugin v0.1.0
*
* Copyright (c) 2009 Tomas Salfischberger, John Resig
* See README for license details
*/
(function($) {
    /* Public function on jQuery elements */
    $.fn.fromTemplate = function(templateId, data, options) {
        var settings = $.extend({}, $.fn.fromTemplate.defaults, options);

        return this.each(function() {
            $(this).html(renderTemplate(templateId, settings, data));
        });
    };

    /* Default settings */
    $.fn.fromTemplate.defaults = {
        modelName: "data"
    };

    /* Private cache */
    var cache = {};

    /* Find template in cache or generate, then render */
    function renderTemplate(templateId, opt, data) {
        var fn = cache[templateId] = cache[templateId] || templateGenerator(templateId, opt);
        return fn(data);
    };

    /* Generate the template function */
    function templateGenerator(templateId, opt) {
        tmpl = $("#" + templateId).html()
        return new Function(opt.modelName,
      "var p=[]; p.push('" +
      tmpl.replace(/[\r\t\n]/g, " ")
          .replace(/'(?=[^#]*#>)/g, "\t")
          .split("'").join("\\'")
          .split("\t").join("'")
          .replace(/<#=(.+?)#>/g, "',$1,'")
          .split("<#").join("');")
          .split("#>").join("p.push('") +
      "');return p.join('');"
    );
    };
})(jQuery);

