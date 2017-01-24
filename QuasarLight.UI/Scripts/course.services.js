var nice = {};
$(document).ready(function () {

    jQuery("body").getNiceScroll().hide();

    jQuery(".card__container").attrchange({
        trackValues: false,
        callback: function (event) {
            if (event.attributeName === "style") {
                if (jQuery(this).css("width") === jQuery(window).width() + "px") {
                    nice = jQuery(".card__container").niceScroll({ horizrailenabled: false, zindex: 999 });
                }
            }
        }
    });

    jQuery(".card__container").on("mouseover mousemove", function () {
        jQuery(this).getNiceScroll().resize();
        jQuery("body").getNiceScroll().hide();
    });

    openCard();
});

var setScroll = function (i) {
    if ($(i).length > 0) jQuery(i).niceScroll().updateScrollBar();
}

function openCard() {
    var courseCard = jQuery(".card");
    courseCard.removeClass("hidden");

    courseCard.offset({ top: $("#cardTop").val(), left: $("#cardLeft").val() });
    var card = new Card("card", $(".card"));
    card.openCard(demo.onCardMove);
}