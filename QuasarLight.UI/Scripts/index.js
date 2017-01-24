if (navigator.userAgent.toLowerCase().indexOf("firefox") > -1) {
    var root = document.getElementsByTagName("html")[0];
    root.setAttribute("class", "ff");
};

var id_menu = new Array("sub_menu_1");
startList = function allclose() {
    document.getElementById(id_menu[i]).style.display = "none";
}

function openMenu(id) {
    for (var i = 0; i < id_menu.length; i++) {
        if (id !== id_menu[i]) {
            document.getElementById(id_menu[i]).style.display = "none";
        }
    }
    if (document.getElementById(id).style.display === "block") {
        document.getElementById(id).style.display = "none";
    } else {
        document.getElementById(id).style.display = "block";
    }
}

new WOW().init();

$(function () {
    $("#myTab a[href=\"#profile\"]").tab("show");
});

$(function () {
    $("#dl-menu").dlmenu();
});
(function (i, s, o, g, r, a, m) {
    i["GoogleAnalyticsObject"] = r;
    i[r] = i[r] || function () {
        (i[r].q = i[r].q || []).push(arguments);
    }, i[r].l = 1 * new Date();
    a = s.createElement(o),
        m = s.getElementsByTagName(o)[0];
    a.async = 1;
    a.src = g;
    m.parentNode.insertBefore(a, m);
})(window, document, "script", "//www.google-analytics.com/analytics.js", "ga");

ga("create", "UA-67609172-1", "auto");
ga("send", "pageview");

/* Start section scrolling */

jQuery(window).scroll(function () {
    var startSection = {};
    if ($("#start-section")[0] !== null) {
        startSection = jQuery("#start-section").offset().top + jQuery("#start-section").height();

        if (jQuery(window).scrollTop() > startSection) {
            jQuery("#tf-menu").hide(100);
        } else {
            jQuery("#tf-menu").show(100);
        }
    }
});

/* Yandex.Metrika counter */
(function (d, w, c) {
    (w[c] = w[c] || []).push(function () {
        try {
            w.yaCounter33702239 = new Ya.Metrika({
                id: 33702239,
                clickmap: true,
                trackLinks: true,
                accurateTrackBounce: true,
                webvisor: true
            });
        } catch (e) { }
    });

    var n = d.getElementsByTagName("script")[0],
        s = d.createElement("script"),
        f = function () { n.parentNode.insertBefore(s, n); };
    s.type = "text/javascript";
    s.async = true;
    s.src = "https://mc.yandex.ru/metrika/watch.js";

    if (w.opera == "[object Opera]") {
        d.addEventListener("DOMContentLoaded", f, false);
    } else { f(); }
})(document, window, "yandex_metrika_callbacks");

/* /Yandex.Metrika counter */