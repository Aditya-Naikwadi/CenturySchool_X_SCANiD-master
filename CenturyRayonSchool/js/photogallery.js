/*-------------------------- Isotope Init --------------------*/
jQuery(window).on("load resize", function (e) {

    var $container = jQuery('.isotope-items'),
        colWidth = function () {
            var w = $container.width(),
                columnNum = 1,
                columnWidth = 0;
            if (w > 1040) {
                columnNum = 5;
            } else if (w > 850) {
                columnNum = 2;
            } else if (w > 768) {
                columnNum = 2;
            } else if (w > 400) {
                columnNum = 2;
            }
            columnWidth = Math.floor(w / columnNum);

            //Isotop Version 1
            var $containerV1 = jQuery('.isotope-items');
            $containerV1.find('.item').each(function () {
                var $item = jQuery(this),
                    multiplier_w = $item.attr('class').match(/item-w(\d)/),
                    multiplier_h = $item.attr('class').match(/item-h(\d)/),
                    width = multiplier_w ? columnWidth * multiplier_w[1] - 10 : columnWidth,
                    height = multiplier_h ? columnWidth * multiplier_h[1] * 0.7 : columnWidth * 0.7;
                $item.css({
                    width: width,
                    height: height,
                });
            });


            return columnWidth;
        },
        isotope = function () {
            $container.isotope({
                resizable: true,
                itemSelector: '.item',
                masonry: {
                    columnWidth: colWidth(),
                    gutterWidth: 10
                }
            });
        };
    isotope();



    // bind filter button click
    jQuery('.isotope-filters').on('click', 'button', function () {
        var filterValue = jQuery(this).attr('data-filter');
        $container.isotope({
            filter: filterValue
        });
    });

    // change active class on buttons
    jQuery('.isotope-filters').each(function (i, buttonGroup) {
        var $buttonGroup = jQuery(buttonGroup);
        $buttonGroup.on('click', 'button', function () {
            $buttonGroup.find('.active').removeClass('active');
            jQuery(this).addClass('active');
        });
    });


    // Masonry Isotope
    var $masonryIsotope = jQuery('.isotope-masonry-items').isotope({
        itemSelector: '.item',
    });

    // bind filter button click
    jQuery('.isotope-filters').on('click', 'button', function () {
        var filterValue = jQuery(this).attr('data-filter');
        $masonryIsotope.isotope({
            filter: filterValue
        });
    });

    // change active class on buttons
    jQuery('.isotope-filters').each(function (i, buttonGroup) {
        var $buttonGroup = jQuery(buttonGroup);
        $buttonGroup.on('click', 'button', function () {
            $buttonGroup.find('.active').removeClass('active');
            jQuery(this).addClass('active');
        });
    });
});