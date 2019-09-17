/*Notas
---------------------------------------------------
 * @001 ACP 11/08/2017 MOTIVO DE CAMBIO: Se evalua si la función popover existe y esta definida.
*/
function sidebarFluid() {
    $("#left").hasClass("sidebar-fixed") && $("#left").removeClass("sidebar-fixed").css({
        height: "auto",
        top: "0",
        left: "auto"
    });
    $("#navigation").hasClass("navbar-fixed-top") && $("#left").css("top", 40);
    $("#left").getNiceScroll().resize().hide();
    $("#left").removeClass("hasScroll")
}

function sidebarFixed() {
    $("#left").addClass("sidebar-fixed");
    $("#left .ui-resizable-handle").css("top", 0);
    $(window).scrollTop() == 0 && $("#left").css("top", 40);
    $("#content").hasClass("container") && $("#left").css("left", $("#content").offset().left);
    $("#left").getNiceScroll().resize().show();
    initSidebarScroll()
}

function topbarFixed() {
    $("#content").addClass("nav-fixed");
    $("#navigation").addClass("navbar-fixed-top");
    $("#left").css("top") == "0px" && $("#left").css("top", 40)
}

function topbarFluid() {
    $("#content").removeClass("nav-fixed");
    $("#navigation").removeClass("navbar-fixed-top");
    $("#left").css("top") == "40px" && !$("#left").hasClass("sidebar-fixed") && $("#left").css("top", 0)
}

function versionFixed() {
    if ($(window).width() >= 1200) {
        $("#content").addClass("container").removeClass("container-fluid");
        $("#navigation .container-fluid").addClass("container").removeClass("container-fluid");
        $("#left").hasClass("sidebar-fixed") && $("#left").css("left", $("#content").offset().left)
    }
}

function versionFluid() {
    $("#content").addClass("container-fluid").removeClass("container");
    $("#navigation .container").addClass("container-fluid").removeClass("container");
    $("body").hasClass("sidebar-right") ? $("#left").css("right", 0) : $("#left").css("left", 0)
}

function slimScrollUpdate(e, t) {
    if (e.length > 0) {
        var n = parseInt(e.attr("data-height")),
            r = e.attr("data-visible") == "true" ? !0 : !1,
            i = e.attr("data-start") == "bottom" ? "bottom" : "top",
            s = {
                height: n,
                color: "#666",
                start: i
            };
        if (r) {
            s.alwaysVisible = !0;
            s.disabledFadeOut = !0
        }
        t !== undefined && (s.scrollTo = t + "px");
        e.slimScroll(s)
    }
}

function destroySlimscroll(e) {
    e.parent().replaceWith(e)
}

function initSidebarScroll() {
    getSidebarScrollHeight();
    if (!$("#left").hasClass("hasScroll")) {
        $("#left").niceScroll({
            cursorborder: 0,
            cursorcolor: "#999",
            railoffset: {
                top: 0,
                left: -2
            },
            autohidemode: !1,
            horizrailenabled: !1
        });
        $("#left").addClass("hasScroll");
        $("#left").on("touchmove", function (e) {
            e.preventDefault()
        })
    } else $("#left").getNiceScroll().resize().show()
}

function getSidebarScrollHeight() {
    var e = $("#left"),
        t = $(window),
        n = $("#navigation"),
        r = t.height();
    if (n.hasClass("navbar-fixed-top") && t.scrollTop() == 0 || t.scrollTop() == 0) r -= 40;
    (e.hasClass("sidebar-fixed") || e.hasClass("mobile-show")) && e.height(r)
}

function checkLeftNav() {
    var e = $(window),
        t = $("#content"),
        n = $("#left");
    if (e.width() <= 840) {
        if (!n.hasClass("mobile-show")) {
            n.hide();
            $("#main").css("margin-left", 0)
        }
        $(".toggle-mobile").length == 0 && $("#navigation .user").before('<a href="#" class="toggle-mobile"><i class="icon-reorder"></i></a>');
        $(".mobile-nav").length == 0 && createSubNav()
    } else {
        if (!n.is(":visible") && !n.hasClass("forced-hide") && !$("#content").hasClass("nav-hidden")) {
            n.show();
            $("#main").css("margin-left", n.width())
        }
        $(".toggle-mobile").remove();
        $(".mobile-nav").removeClass("open");
        if (t.hasClass("forced-fixed")) {
            t.removeClass("nav-fixed");
            $("#navigation").removeClass("navbar-fixed-top")
        }
        if (e.width() < 1200) {
            if ($("#navigation .container").length > 0) {
                versionFluid();
                $("body").addClass("forced-fluid")
            }
        } else $("body").hasClass("forced-fluid") && versionFixed()
    }
}

function resizeHandlerHeight() {
    var e = $(window).height(),
        t = $(window).scrollTop() == 0 ? 40 : 0;
    $("#left .ui-resizable-handle").height(e - t)
}

function toggleMobileNav() {
    var e = $(".mobile-nav");
    e.toggleClass("open");
    e.find(".open").removeClass("open")
}

function getNavElement(e) {
    var t = $.trim(e.find(">a").text()),
        n = "";
    n += "<li><a href='" + e.find(">a").attr("href") + "'>" + t + "</a>";
    e.find(">.dropdown-menu").length > 0 && (n += getNav(e.find(">.dropdown-menu")));
    n += "</li>";
    return n
}

function getNav(e) {
    var t = "";
    t += "<ul>";
    e.find(">li").each(function () {
        t += getNavElement($(this))
    });
    t += "</ul>";
    nav = t;
    return t
}

function createSubNav() {
    if ($(".mobile-nav").length == 0) {
        var e = $("#navigation .main-nav"),
            t = e;
        getNav(t);
        $("#navigation").append(nav);
        $("#navigation > ul").last().addClass("mobile-nav");
        $(".mobile-nav > li > a").click(function (e) {
            var t = $(this);
            $("#navigation").getNiceScroll().resize().show();

            if (t.next().length !== 0) {
                e.preventDefault();
                var n = t.next();
                t.parents(".mobile-nav").find(".open").not(n).each(function () {
                    var e = $(this);
                    e.removeClass("open");
                    e.prev().find("i").removeClass("icon-angle-down").addClass("icon-angle-left")
                });
                n.toggleClass("open");
                t.find("i").toggleClass("icon-angle-left").toggleClass("icon-angle-down")
            }
        })
    }
}

function hideNav() {
    $("#left").toggle().toggleClass("forced-hide");
    $("#left").is(":visible") ? $("#main").css("margin-left", $("#left").width()) : $("#main").css("margin-left", 0);
    if ($(".dataTable").length > 0) {
        var e = $.fn.dataTable.fnTables(!0);
        if (e.length > 0) {
            $(e).each(function () {
                $(this).hasClass("dataTable-scroller") && $(this).dataTable().fnDraw()
            });
            $(e).dataTable().fnAdjustColumnSizing()
        }
    }
    $(".calendar").length > 0 && $(".calendar").fullCalendar("render")
}

function scrolledClone(e, t) {
    t.remove();
    e.parent().removeClass("open")
}

function resizeContent() {
    if ($("#main").height() < $(window).height()) {
        var e = 40;
        $("#footer").length > 0 && (e += $("#footer").outerHeight());
        $("#content").css({
            "min-height": "auto",
            height: $(window).height() - e
        })
    }
    if ($("#left").height() > $("#main").height() && $("#main").height() < $(window).height()) {
        $("#left").addClass("full");
        $("#footer").css({
            position: "fixed",
            bottom: 0,
            top: "auto"
        })
    }
    if ($("#left").height() < $(window).height() && !$("#left").hasClass("force-full")) {
        $("#left").removeClass("full");
        $("#footer").attr("style", "")
    }
}
var nav = "";
$(document).ready(function () {
    resizeContent();
    if ($(".username-check").length > 0) {
        var e;
        $(".username-check").change(function (e) {
            var t = $(this);
            t.parent().next().html("<i class='icon-spinner icon-spin'></i> Checking availability...");
            $.post("/check", {
                username: t.val()
            }, function (e) {
                if (e.available == "true") {
                    t.parent().next().html("<i class='icon-ok'></i> Username is available!");
                    t.parents(".control-group").removeClass("error").addClass("success")
                } else {
                    t.parent().next().html("<i class='icon-remove'></i> Username not available!");
                    t.parents(".control-group").removeClass("success").addClass("error")
                }
            }, "json")
        });
        $(".username-check-force").click(function (e) {
            e.preventDefault();
            $(".username-check").trigger("change")
        });
        $(".username-check").keyup(function (t) {
            clearTimeout(e);
            e = setTimeout(function () {
                $(".username-check").trigger("change")
            }, 500)
        })
    }
    $(".gallery-dynamic").length > 0 && $(".gallery-dynamic").imagesLoaded(function () {
        $(".gallery-dynamic").masonry({
            itemSelector: "li",
            columnWidth: 201,
            isAnimated: !0
        })
    });
    $(".gototop").click(function (e) {
        e.preventDefault();
        $("html, body").animate({
            scrollTop: 0
        }, 600)
    });
    $("body").attr("data-mobile-sidebar") == "slide" && $("body").touchwipe({
        wipeRight: function () {
            $("#left").show().addClass("mobile-show");
            initSidebarScroll()
        },
        wipeLeft: function () {
            $("#left").hide().removeClass("mobile-show")
        },
        preventDefaultEvents: !1
    });
    $("body").attr("data-mobile-sidebar") == "button" && $(".mobile-sidebar-toggle").click(function (e) {
        e.preventDefault();
        $("#left").toggle().toggleClass("mobile-show");
        initSidebarScroll()
    });
    $(".main-nav > li, .subnav-menu > li").hover(function () {
        if ($(this).attr("data-trigger") == "hover")
            if ($(this).parents(".subnav-menu").length > 0 && $("#left").hasClass("sidebar-fixed")) $(this).find(">a").trigger("click");
            else {
                $(this).closest(".dropdown-menu").stop(!0, !0).show();
                $(this).addClass("open")
            }
    }, function () {
        if ($(this).attr("data-trigger") == "hover") {
            $(this).closest(".dropdown-menu").stop(!0, !0).hide();
            $(this).removeClass("open")
        }
    });
    $(".subnav-menu > li > a[data-toggle=dropdown]").click(function () {
        var e = $(this);
        if ($("#left").hasClass("sidebar-fixed") || $("#left").hasClass("mobile-show")) {
            $(".cloned").remove();
            var t = e.next(),
                n = e.offset(),
                r = t.clone().css({
                    top: n.top,
                    left: n.left + $("#left").width()
                }).show().addClass("cloned");
            $("body").append(r);
            t.hide();
            $("#left").scroll(function () {
                scrolledClone(e, r)
            });
            $(window).scroll(function () {
                scrolledClone(e, r)
            });
            $("body").click(function (t) {
                var n = $(t.target);
                if (n.parents(".cloned").length == 0 && n.attr("data-toggle") != "dropdown") {
                    e.parent().removeClass("open");
                    r.remove()
                }
            })
        }
    });
    $("body").on("click", ".change-input", function (e) {
        e.preventDefault();
        var t = $(this),
            n = t.parent().prev(),
            r = t.parent().clone();
        r.html(n.clone().val(""));
        n.after(r);
        t.addClass("btn-satgreen update-input").removeClass("btn-grey-4 change-input").text("Update")
    });
    $("body").on("click", ".update-input", function (e) {
        e.preventDefault();
        var t = $(this),
            n = t.parent();
        t.after('<span><i class="icon-spinner icon-spin"></i>Updating...</span>');
        setTimeout(function () {
            n.find("span").remove();
            n.prev().slideUp(200, function () {
                n.prev().remove();
                t.removeClass("update-input btn-satgreen").addClass("btn-grey-4 change-input").text("Change")
            })
        }, 1e3)
    });
    $(".subnav-hidden").each(function () {
        $(this).find(".subnav-menu").is(":visible") && $(this).find(".subnav-menu").hide()
    });
    setTimeout(function () {
        slimScrollUpdate($(".messages").parent(), 9999)
    }, 1e3);
    createSubNav();
    $(".breadcrumbs .close-bread > a").click(function (e) {
        e.preventDefault();
        $(".breadcrumbs").fadeOut()
    });
    $("#navigation").on("click", ".toggle-mobile", function (e) {
        e.preventDefault();
        toggleMobileNav()
    });
    $(".content-slideUp").click(function (e) {
        e.preventDefault();
        var t = $(this),
            n = t.parents(".box").find(".box-content");
        n.slideToggle("fast", function () {
            t.find("i").toggleClass("icon-angle-up").toggleClass("icon-angle-down");
            t.find("i").hasClass("icon-angle-up") ? n.hasClass("scrollable") && destroySlimscroll(n) : n.hasClass("scrollable") && slimScrollUpdate(n)
        })
    });
    $(".content-remove").click(function (e) {
        e.preventDefault();
        var t = $(this),
            n = t.parents("[class*=span]"),
            r = parseInt(n.attr("class").replace("span", "")),
            i = n.prev().length > 0 ? n.prev() : n.next();
        if (i.length > 0) var s = parseInt(i.attr("class").replace("span", ""));
        bootbox.animate(!1);
        bootbox.confirm("Do you really want to remove the widget <strong>" + t.parents(".box-title").find("h3").text() + "</strong>?", "Cancel", "Yes, remove", function (e) {
            if (e) {
                t.parents("[class*=span]").remove();
                i.length > 0 && i.removeClass("span" + s).addClass("span" + (s + r))
            }
        })
    });
    $(".content-refresh").click(function (e) {
        e.preventDefault();
        var t = $(this);
        t.find("i").addClass("icon-spin");
        setTimeout(function () {
            t.find("i").removeClass("icon-spin")
        }, 2e3)
    });
    $("#vmap").length > 0 && $("#vmap").vectorMap({
        map: "world_en",
        backgroundColor: null,
        color: "#ffffff",
        hoverOpacity: .7,
        selectedColor: "#2d91ef",
        enableZoom: !0,
        showTooltip: !1,
        values: sample_data,
        scaleColors: ["#8cc3f6", "#5c86ac"],
        normalizeFunction: "polynomial",
        onRegionClick: function () {
            alert("This Region has " + (Math.floor(Math.random() * 10) + 1) + " users!")
        }
    });
    $(".custom-checkbox").each(function () {
        var e = $(this);
        e.hasClass("checkbox-active") && e.find("i").toggleClass("icon-check-empty").toggleClass("icon-check");
        e.bind("click", function (t) {
            t.preventDefault();
            e.find("i").toggleClass("icon-check-empty").toggleClass("icon-check");
            e.toggleClass("checkbox-active")
        })
    });
    $(".tasklist").on("click", "li", function (e) {
        var t = $(this),
            n = $(this).find("input[type=checkbox]").first();
        t.toggleClass("done");
        e.target.nodeName == "LABEL" && e.preventDefault();
        if (e.target.nodeName != "INS" && e.target.nodeName != "INPUT") {
            n.prop("checked", !n.prop("checked"));
            $(".tasklist input").iCheck("update")
        }
    });
    $(".tasklist").on("is.Changed", "input[type=checkbox]", function () {
        $(this).parents("li").toggleClass("done")
    });
    if ($("#new-task .select2-me").length > 0) {
        function t(e) {
            return e.id ? "<i class='" + e.text + "'></i> ." + e.text : e.text
        }
        $("#new-task .select2-me").select2({
            formatResult: t,
            formatSelection: t,
            escapeMarkup: function (e) {
                return e
            }
        })
    }
    $(".tasklist").on("click", ".task-bookmark", function (e) {
        var t = $(this),
            n = $(this).parents("li"),
            r = $(this).parents("ul");
        e.preventDefault();
        e.stopPropagation();
        n.toggleClass("bookmarked");
        n.hasClass("bookmarked") ? n.fadeOut(200, function () {
            n.prependTo(r).fadeIn()
        }) : r.find(".bookmarked").length > 0 ? n.fadeOut(200, function () {
            n.insertAfter(r.find(".bookmarked").last()).fadeIn()
        }) : n.fadeOut(200, function () {
            n.prependTo(r).fadeIn()
        })
    });
    $(".tasklist").on("click", ".task-delete", function (e) {
        e.preventDefault();
        e.stopPropagation();
        var t = $(this);
        t.parents("li").fadeOut()
    });
    $(".tasklist").sortable({
        items: "li",
        opacity: .7,
        placeholder: "widget-placeholder-2",
        forcePlaceholderSize: !0,
        tolerance: "pointer"
    });
    $(".sortable-box").sortable({
        connectWith: ".box",
        items: ".box",
        opacity: .7,
        placeholder: "widget-placeholder",
        forcePlaceholderSize: !0,
        tolerance: "pointer",
        dropOnEmpty: !0
    });
    $(".toggle-subnav").click(function (e) {
        e.preventDefault();
        var t = $(this);
        t.parents(".subnav").toggleClass("subnav-hidden").find(".subnav-menu,.subnav-content").slideToggle("fast");
        t.find("i").toggleClass("icon-angle-down").toggleClass("icon-angle-right");
        if ($("#left").hasClass("mobile-show") || $("#left").hasClass("sidebar-fixed")) {
            getSidebarScrollHeight();
            $("#left").getNiceScroll().resize().show()
        }
    });
    $("#left").sortable({
        items: ".subnav",
        placeholder: "widget-placeholder",
        forcePlaceholderSize: !0,
        axis: "y",
        handle: ".subnav-title",
        tolerance: "pointer"
    });
    $(".scrollable").length > 0 && $(".scrollable").each(function () {
        var e = $(this),
            t = parseInt(e.attr("data-height")),
            n = e.attr("data-visible") == "true" ? !0 : !1,
            r = e.attr("data-start") == "bottom" ? "bottom" : "top",
            i = {
                height: t,
                color: "#666",
                start: r,
                allowPageScroll: !0
            };
        if (n) {
            i.alwaysVisible = !0;
            i.disabledFadeOut = !0
        }
        e.slimScroll(i)
    });
    $(".new-task-form").submit(function (e) {
        e.preventDefault();
        $("#new-task").modal("hide");
        var t = $(this),
            n = $(".tasklist"),
            r = t.find("select[name=icons]"),
            i = t.find("input[name=task-name]"),
            s = t.find("input[name=task-bookmarked]");
        if (i.val() != "") {
            var o = "";
            s.is(":checked") ? o += "<li class='bookmarked'>" : o += "<li>";
            o += '<div class="check"><input type="checkbox" class="icheck-me" data-skin="square" data-color="blue"></div><span class="task"><i class="' + r.select2("val") + '"></i><span>' + i.val() + '</span></span><span class="task-actions"><a href="#" class="task-delete" rel="tooltip" title="Delete that task"><i class="icon-remove"></i></a><a href="#" class="task-bookmark" rel="tooltip" title="Mark as important"><i class="icon-bookmark-empty"></i></a></span></li>';
            n.find(".bookmarked").length > 0 ? s.is(":checked") ? n.find(".bookmarked").first().before(o) : n.find(".bookmarked").last().after(o) : n.prepend(o);
            icheck();
            n.find("[rel=tooltip]").tooltip();
            r.select2("val", "icon-adjust");
            i.val("");
            s.prop("checked", !1)
        }
    });
    $("#message-form .text input").on("focus", function (e) {
        var t = $(this);
        t.parents(".messages").find(".typing").addClass("active").find(".name").html("John Doe");
        slimScrollUpdate(t.parents(".scrollable"), 1e5)
    });
    $("#message-form .text input").on("blur", function (e) {
        var t = $(this);
        t.parents(".messages").find(".typing").removeClass("active");
        slimScrollUpdate(t.parents(".scrollable"), 1e5)
    });
    $(".jq-datepicker").length > 0 && $(".jq-datepicker").datepicker({
        showOtherMonths: !0,
        selectOtherMonths: !0,
        prevText: "",
        nextText: ""
    });
    $(".spark-me").length > 0 && $(".spark-me").sparkline("html", {
        height: "25px",
        enableTagOptions: !0
    });
    $("#left").hasClass("no-resize") || $("#left").resizable({
        minWidth: 60,
        handles: "e",
        resize: function (e, t) {
            var n = $(".search-form .search-pane input[type=text]"),
                r = $("#main");
            n.css({
                width: t.size.width - 55
            });
            if (Math.abs(200 - t.size.width) <= 20) {
                $("#left").css("width", 200);
                n.css("width", 145);
                r.css("margin-left", 200)
            } else r.css("margin-left", $("#left").width())
        },
        stop: function () {
            $("#left .ui-resizable-handle").css("background", "none")
        },
        start: function () {
            $("#left .ui-resizable-handle").css("background", "#aaa")
        }
    });
    //@001 I
    if( typeof popover !== 'undefined' && jQuery.isFunction(popover) ) {
        //Es seguro ejectura la función
        $("[rel=popover]").popover();
    }
    //$("[rel=popover]").popover();
    //@001 I
    $(".toggle-nav").click(function (e) {
        e.preventDefault();
        hideNav()
    });
    $("#content").hasClass("nav-hidden") && hideNav();
    $(".table-mail .sel-star").click(function (e) {
        e.preventDefault();
        e.stopPropagation();
        var t = $(this);
        t.toggleClass("active")
    });
    $(".table .sel-all").change(function (e) {
        e.preventDefault();
        e.stopPropagation();
        var t = $(this);
        t.parents(".table").find("tbody .selectable").prop("checked", el.prop("checked"))
    });
    $(".table-mail > tbody > tr").click(function (e) {
        var t = $(this),
            n = t.find(".table-checkbox > input");
        t.toggleClass("warning");
        e.target.nodeName != "INPUT" && n.prop("checked", !n.prop("checked"))
    });
    resizeHandlerHeight();
    $(".table .alpha").click(function (e) {
        e.preventDefault();
        var t = $(this),
            n = "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            r = "",
            i = [];
        t.parents().find(".alpha .alpha-val span").each(function () {
            i.push($(this).text())
        });
        r += "<li class='active'><span>All</span></li>";
        for (var s = 0; s < n.length; s++) {
            var o = $.inArray(n.charAt(s), i) != -1 ? " class='active'" : "";
            r += "<li" + o + "><span>" + n.charAt(s) + "</span></li>"
        }
        t.parents(".table").before("<div class='letterbox'><ul class='letter'>" + r + "</ul></div>");
        $(".letterbox .letter > .active").click(function () {
            var e = $(this);
            if (e.text() != "All") {
                slimScrollUpdate(e.parents(".scrollable"), 0);
                var t = e.parents(".box-content").find(".table .alpha:contains('" + e.text() + "')");
                slimScrollUpdate(e.parents(".scrollable"), t.position().top)
            }
            e.parents(".letterbox").remove()
        })
    });
    $(".theme-colors > li > span").hover(function (e) {
        var t = $(this),
            n = $("body");
        n.attr("class", "").addClass("theme-" + t.attr("class"))
    }, function () {
        var e = $(this),
            t = $("body");
        t.attr("data-theme") !== undefined ? t.attr("class", "").addClass(t.attr("data-theme")) : t.attr("class", "")
    }).click(function () {
        var e = $(this);
        $("body").addClass("theme-" + e.attr("class")).attr("data-theme", "theme-" + e.attr("class"))
    });
    $(".version-toggle > a").click(function (e) {
        e.preventDefault();
        e.stopPropagation();
        var t = $(this),
            n = t.parent();
        if (!t.hasClass("active")) {
            n.find(".active").removeClass("active");
            t.addClass("active")
        }
        t.hasClass("set-fixed") ? versionFixed() : versionFluid()
    });
    $(".topbar-toggle > a").click(function (e) {
        e.preventDefault();
        e.stopPropagation();
        var t = $(this),
            n = t.parent();
        if (!t.hasClass("active")) {
            n.find(".active").removeClass("active");
            t.addClass("active")
        }
        t.hasClass("set-topbar-fixed") ? topbarFixed() : topbarFluid()
    });
    $(".sidebar-toggle > a").click(function (e) {
        e.preventDefault();
        e.stopPropagation();
        var t = $(this),
            n = t.parent();
        if (!t.hasClass("active")) {
            n.find(".active").removeClass("active");
            t.addClass("active")
        }
        $(".search-form .search-pane input").attr("style", "");
        $("#main").attr("style", "");
        t.hasClass("set-sidebar-fixed") ? sidebarFixed() : sidebarFluid()
    });
    $(".del-gallery-pic").click(function (e) {
        e.preventDefault();
        var t = $(this),
            n = t.parents("li");
        n.fadeOut(400, function () {
            n.remove()
        })
    });
    checkLeftNav();
    $("body").attr("data-layout") == "fixed" && versionFixed();
    $("body").attr("data-layout-topbar") == "fixed" && topbarFixed();
    $("body").attr("data-layout-sidebar") == "fixed" && sidebarFixed()
});
$.fn.scrollBottom = function () {
    return $(document).height() - this.scrollTop() - this.height()
};
$(window).scroll(function (e) {
    var t = 0,
        n = $(window),
        r = $(document);
    if (n.scrollTop() == 0 || $("#left").hasClass("sidebar-fixed")) $("#left .ui-resizable-handle").css("top", t);
    else {
        n.scrollTop() + $("#left .ui-resizable-handle").height() <= r.height() ? t = n.scrollTop() - 40 : t = r.height() - $("#left .ui-resizable-handle").height() - 40;
        $("#left .ui-resizable-handle").css("top", t)
    } !$("#content").hasClass("nav-fixed") && $("#left").hasClass("sidebar-fixed") && (n.scrollTop() < 40 ? $("#left").css("top", 40 - n.scrollTop()) : $("#left").css("top", 0));
    getSidebarScrollHeight();
    resizeHandlerHeight()
});
$(window).resize(function (e) {
    checkLeftNav();
    getSidebarScrollHeight();
    resizeContent();
    resizeHandlerHeight()
});