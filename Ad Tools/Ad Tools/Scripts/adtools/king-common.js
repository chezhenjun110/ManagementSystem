$(document).ready(function(){
    if (document.getElementById("example") != null) {
        $('#example').dataTable({
            //跟数组下标一样，第一列从0开始，这里表格初始化时，第四列默认降序
            "order": [3, "desc"]
        });
    }
    
    if (document.getElementById("datebox") != null) {
        $('#datebox').datebox({
            required: true,
            editable: false,
           

        });
    }
   
    if (document.getElementById("combotree") != null) {
        $('#combotree').combotree({
            url: '/UserManagement/inite_Outree',
            required: true,
            onSelect: function (node) {
                //alert(node.id);
                document.getElementById("ChoosedOu").innerHTML = node.path;
                if (document.getElementById("modifybtn") != null) {
                    document.getElementById("modifybtn").disabled = false;
                }
            }
        });
    }
    id = $.cookie('activeid');
    var currenttext = $.cookie('currenttext');
    var firsttext = $.cookie('firsttext');
    //var secondtext = $.cookie('secondtext');
    //设置菜单是否可用
    $.ajax({
        url: '/_Layout/UserPermission',
        type: 'get',
        success: function (data) {
    
            if (data[0]=="0")
            {
                for (var i = 1; i < 14; i++) {
                    $('#' + i).addClass('guide');
                }
            }
            {
                var j = 0;
                for (var i = 1; i < 14; i++) {
                    if (i < data[j]) {

                        $('#' + i).addClass('guide');
                    }
                    else {
                        if (i == data[j]) {
                            $('#' + i).addClass('unguide');
                            j = j + 1;
                            if (j == data.length) {
                                for (var a = i + 1; a < 14; a++) {
                                    $('#' + a).addClass('guide');
                                }
                                break;
                            }
                        }
                    }
                }
            }
            $('.guide').click(function (e) {
                
                //this==e.target
                //e.preventDefault();
                //获取点击项的id
                var id = $(this).attr('id');
                //菜单点击后展开  原来展开的闭合
                $('.active .guide').parent('li').removeClass('active');
                $(this).parent('li').addClass('active');
                //当前点击项的文本
                var currenttext = $(this).children('span').html();
                //上一级的文本
                $ul = $(this).parent('li').parent('.sub-menu ');
                $a = $ul.siblings('a');
                $span = $a.children('span');
                var firsttext = $span.html();
                //若有三级 此为最外面的文本
                //var secondtext = $a.parent('li').parent('.sub-menu ').siblings('a').children('span').html();//三级的最外面一级
               
                $.cookie('activeid', id, { path: "/" });//当前点击项id
                $.cookie('currenttext', currenttext, { path: "/" });
                $.cookie('firsttext', firsttext, { path: "/" });
                /*if (secondtext == null) {
                    $.cookie('secondtext', null, { path: "/" });
                }
                else {
                    $.cookie('secondtext', secondtext, { path: "/" });
                }
                */
            }
);
            $('.unguide').click(function (e) {

                return false;
            }
        );
        },
        error: function () {
            //alert("failed");
        }
    });
    $.ajax({
        url: '/_Layout/DeleteButton',
        type: 'get',
        success: function (data) {
            if (data.Message=="false") {

                if (document.getElementById("deletebtn") != null) {
                    document.getElementById("deletebtn").style.visibility = "hidden";
                }

            }
        },
        error: function () {
            //alert("failed");
        }
    });
    //菜单打开与否
    if(id==null)
    {
        
    }
    else {
        $('#' + id).parent('li').addClass('active');
        $firstli = $('#' + id).parent('li').parent('.sub-menu ').parent('li');
        $firstli.addClass('active');
        $firstli.children('.js-sub-menu-toggle').children('.toggle-icon').removeClass('fa-angle-right').addClass('fa-angle-down');
        $firstli.children('ul').slideToggle(0);
        $secondli = $firstli.parent('.sub-menu ').parent('li');
        if ($secondli != null) {
            $secondli.addClass('active');
            $secondli.children('.js-sub-menu-toggle').children('.toggle-icon').removeClass('fa-angle-right').addClass('fa-angle-down');
            $secondli.children('ul').slideToggle(0);
        }
        $('.breadcrumb').empty();


            $('.breadcrumb').append('<li class="first"><span></span><a href="~/#">' + firsttext + '</a></li><li class="active"><span></span>' + currenttext + '</li>');

    }

    //排序事件
    $('.topl').click(function (e) {
        if ($(this).hasClass('tchosed')) {
            if ($(this).hasClass('sort_asc')) {
                //原本为升序
                $(this).removeClass('sort_asc').addClass('sort_desc');
                //$(this).siblings('.botm').addClass('bchosed');
            }
            else {
                if ($(this).hasClass('sort_desc')) {
                    //原本为降序
                    $(this).removeClass('sort_desc').addClass('sort_asc');

                }
                else {
                    //原本无排序
                    $('.sort_asc').removeClass('sort_asc').addClass('sort');
                    $('.sort_desc').removeClass('sort_desc').addClass('sort');
                    $(this).removeClass('sort').addClass('sort_asc');


                }
            }
        }
        else {
            $('.tchosed').removeClass('tchosed');
            $('.bchosed').removeClass('bchosed');
            $(this).addClass('tchosed');
            $(this).siblings('.botm').addClass('bchosed');
            if ($(this).hasClass('sort_asc')) {
                //原本为升序
                $(this).removeClass('sort_asc').addClass('sort_desc');

            }
            else {
                if ($(this).hasClass('sort_desc')) {
                    //原本为降序
                    $(this).removeClass('sort_desc').addClass('sort_asc');

                }
                else {
                    //原本无排序
                    $('.sort_asc').removeClass('sort_asc').addClass('sort');
                    $('.sort_desc').removeClass('sort_desc').addClass('sort');
                    $(this).removeClass('sort').addClass('sort_asc');
                }
            }
        }
    });


	/************************
	/*	MAIN NAVIGATION
	/************************/

	$('.main-menu .js-sub-menu-toggle').click( function(e){

		e.preventDefault();

		$li = $(this).parent('li');/*改了*/
		if (!$li.hasClass('active')) {

		    if ($li.siblings('.active') != null)
		    {
		        $li.siblings('.active').children('ul').slideToggle(300);
		        $li.siblings('.active').children('.js-sub-menu-toggle').children('.toggle-icon').removeClass('fa-angle-down').addClass('fa-angle-right');
		        $li.siblings('.active').removeClass('active');
		    }
		    $li.children('.js-sub-menu-toggle').children('.toggle-icon').removeClass('fa-angle-right').addClass('fa-angle-down');//fa-angle-right
		    $li.addClass('active');
		    $li.children('ul').slideToggle(300);
            
		}
		else {
		    $li.children('.js-sub-menu-toggle').children('.toggle-icon').removeClass('fa-angle-down').addClass('fa-angle-right');//fa-angle-right
		    $li.removeClass('active');
		    $li.children('ul').slideToggle(300);
		}

	});
	

	$('.js-toggle-minified').clickToggle(
		function() {
			$('.left-sidebar').addClass('minified');
			$('.content-wrapper').addClass('expanded');

			$('.left-sidebar .sub-menu')
			.css('display', 'none')
			.css('overflow', 'hidden'); 
			
			$('.sidebar-minified').find('i.fa-angle-left').toggleClass('fa-angle-right');
		},
		function() {
			$('.left-sidebar').removeClass('minified');
			$('.content-wrapper').removeClass('expanded');
			$('.sidebar-minified').find('i.fa-angle-left').toggleClass('fa-angle-right');
		}
	);

	// main responsive nav toggle
	$('.main-nav-toggle').clickToggle(
		function() {
			$('.left-sidebar').slideDown(300)
		},
		function() {
			$('.left-sidebar').slideUp(300);
		}
	);


	//*******************************************
	/*	LIVE SEARCH
	/********************************************/

	$mainContentCopy = $('.main-content').clone();
	$('.searchbox input[type="search"]').keydown( function(e) {
		var $this = $(this);
		
		setTimeout(function() {
			var query = $this.val();
			
			if( query.length > 2 ) {
				var regex = new RegExp(query, "i");
				var filteredWidget = [];

				$('.widget-header h3').each( function(index, el){
					var matches = $(this).text().match(regex);

					if( matches != "" && matches != null ) {
						filteredWidget.push( $(this).parents('.widget') );
					}
				});
				
				if( filteredWidget.length > 0 ) {
					$('.main-content .widget').hide();
					$.each( filteredWidget, function(key, widget) {
						widget.show();
					});
				}else{
					console.log('widget not found');
				}
			}else {
				$('.main-content .widget').show();
			}
		}, 0);
	});

	// widget remove
	$('.widget .btn-remove').click(function(e){

		e.preventDefault();
		$(this).parents('.widget').fadeOut(300, function(){
			$(this).remove();
		});
	});

	// widget toggle expand
	$('.widget .btn-toggle-expand').clickToggle(
		function(e) {
			e.preventDefault();
			$(this).parents('.widget').find('.widget-content').slideUp(300);
			$(this).find('i.fa-chevron-up').toggleClass('fa-chevron-down');
		},
		function(e) {
			e.preventDefault();
			$(this).parents('.widget').find('.widget-content').slideDown(300);
			$(this).find('i.fa-chevron-up').toggleClass('fa-chevron-down');
		}
	);

	// widget focus
	$('.widget .btn-focus').clickToggle(
		function(e) {
			e.preventDefault();
			$(this).find('i.fa-eye').toggleClass('fa-eye-slash');
			$(this).parents('.widget').find('.btn-remove').addClass('link-disabled');
			$(this).parents('.widget').addClass('widget-focus-enabled');
			$('<div id="focus-overlay"></div>').hide().appendTo('body').fadeIn(300);

		},
		function(e) {
			e.preventDefault();
			$theWidget = $(this).parents('.widget');
			
			$(this).find('i.fa-eye').toggleClass('fa-eye-slash');
			$theWidget.find('.btn-remove').removeClass('link-disabled');
			$('body').find('#focus-overlay').fadeOut(function(){
				$(this).remove();
				$theWidget.removeClass('widget-focus-enabled');
			});
		}
	);


	/************************
	/*	WINDOW RESIZE
	/************************/

	$(window).bind("resize", resizeResponse);

	function resizeResponse() {

		if( $(window).width() < (992-15)) {
			if( $('.left-sidebar').hasClass('minified') ) {
				$('.left-sidebar').removeClass('minified');
				$('.left-sidebar').addClass('init-minified');
			}

		}else {
			if( $('.left-sidebar').hasClass('init-minified') ) {
				$('.left-sidebar')
				.removeClass('init-minified')
				.addClass('minified');
			}
		}
	}


	/************************
	/*	BOOTSTRAP TOOLTIP
	/************************/

	$('body').tooltip({
		selector: "[data-toggle=tooltip]",
		container: "body"
	});


	/************************
	/*	BOOTSTRAP ALERT
	/************************/

	$('.alert .close').click( function(e){
		e.preventDefault();
		$(this).parents('.alert').fadeOut(300);
	});


	/************************
	/*	BOOTSTRAP POPOVER
	/************************/

	$('.btn-help').popover({
		container: 'body',
		placement: 'top',
		html: true,
		title: '<i class="fa fa-book"></i> Help',
		content: "Help summary goes here. Options can be passed via data attributes <code>data-</code> or JavaScript. Please check <a href='http://getbootstrap.com/javascript/#popovers'>Bootstrap Doc</a>"
	});

	$('.demo-popover1 #popover-title').popover({
		html: true,
		title: '<i class="fa fa-cogs"></i> Popover Title',
		content: 'This popover has title and support HTML content. Quickly implement process-centric networks rather than compelling potentialities. Objectively reinvent competitive technologies after high standards in process improvements. Phosfluorescently cultivate 24/365.'
	});

	$('.demo-popover1 #popover-hover').popover({
		html: true,
		title: '<i class="fa fa-cogs"></i> Popover Title',
		trigger: 'hover',
		content: 'Activate the popover on hover. Objectively enable optimal opportunities without market positioning expertise. Assertively optimize multidisciplinary benefits rather than holistic experiences. Credibly underwhelm real-time paradigms with.'
	});

	$('.demo-popover2 .btn').popover();
	

	/*****************************
	/*	WIDGET WITH AJAX ENABLE
	/*****************************/

	$('.widget-header-toolbar .btn-ajax').click( function(e){
		e.preventDefault();
		$theButton = $(this);

		$.ajax({
			url: 'php/widget-ajax.php',
			type: 'POST',
			dataType: 'json',
			cache: false,
			beforeSend: function(){
				$theButton.prop('disabled', true);
				$theButton.find('i').removeClass().addClass('fa fa-spinner fa-spin');
				$theButton.find('span').text('Loading...');
			},
			success: function( data, textStatus, XMLHttpRequest ) {
				
				setTimeout( function() {
					getResponseAction($theButton, data['msg'])
				}, 1000 );
				/* setTimeout is used for demo purpose only */

			},
			error: function( XMLHttpRequest, textStatus, errorThrown ) {
				console.log("AJAX ERROR: \n" + errorThrown);
			}
		});
	});
	
	function getResponseAction(theButton, msg){

		$('.widget-ajax .alert').removeClass('alert-info').addClass('alert-success')
		.find('span').text( msg );

		$('.widget-ajax .alert').find('i').removeClass().addClass('fa fa-check-circle');

		theButton.prop('disabled', false);
		theButton.find('i').removeClass().addClass('fa fa-floppy-o');
		theButton.find('span').text('Update');
	}


	/**************************************
	/*	MULTISELECT/SINGLESELECT DROPDOWN
	/**************************************/

	if( $('.widget-header .multiselect').length > 0 ) {

		$('.widget-header .multiselect').multiselect({
			dropRight: true,
			buttonClass: 'btn btn-warning btn-sm'
		});
	}

	//*******************************************
	/*	SWITCH INIT
	/********************************************/

	if( $('.bs-switch').length > 0 ) {
		$('.bs-switch').bootstrapSwitch();
	}

	/* set minimum height for the left content wrapper, demo purpose only  */
	if( $('.demo-only-page-blank').length > 0 ) {
		$('.content-wrapper').css('min-height', $('.wrapper').outerHeight(true) - $('.top-bar').outerHeight(true));
	}
	

	
	/************************
	/*	TOP BAR
	/************************/

	if( $('.top-general-alert').length > 0 ) {

		if(localStorage.getItem('general-alert') == null) {
			$('.top-general-alert').delay(800).slideDown('medium');
			$('.top-general-alert .close').click( function() {
				$(this).parent().slideUp('fast');
				localStorage.setItem('general-alert', 'closed');
			});
		}
	}

	//*******************************************
	/*	SELECT2
	/********************************************/

	if( $('.select2').length > 0) {
		$('.select2').select2();
	}

	if( $('.select2-multiple').length > 0) {
		$('.select2-multiple').select2();
	}

});

// toggle function
$.fn.clickToggle = function( f1, f2 ) {
	return this.each( function() {
		var clicked = false;
		$(this).bind('click', function() {
			if(clicked) {
				clicked = false;
				return f2.apply(this, arguments);
			}

			clicked = true;
			return f1.apply(this, arguments);
		});
	});

}