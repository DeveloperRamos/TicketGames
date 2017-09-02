/* SHARED VARS */
var firstrun = true,
    touch = false,
    clickEv = 'click';


var isMobile = {
    Android: function() {
        return navigator.userAgent.match(/Android/i);
    },
    BlackBerry: function() {
        return navigator.userAgent.match(/BlackBerry/i);
    },
    iOS: function() {
        return navigator.userAgent.match(/iPhone|iPad|iPod/i);
    },
    Opera: function() {
        return navigator.userAgent.match(/Opera Mini/i);
    },
    Windows: function() {
        return navigator.userAgent.match(/IEMobile/i);
    },
    any: function() {
        return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
    }
};

/* Fucntion get width browser */
function getWidthBrowser() {
	var myWidth;

	if( typeof( window.innerWidth ) == 'number' ) { 
		//Non-IE 
		myWidth = window.innerWidth;
		//myHeight = window.innerHeight; 
	} 
	else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight ) ) { 
		//IE 6+ in 'standards compliant mode' 
		myWidth = document.documentElement.clientWidth; 
		//myHeight = document.documentElement.clientHeight; 
	} 
	else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ) { 
		//IE 4 compatible 
		myWidth = document.body.clientWidth; 
		//myHeight = document.body.clientHeight; 
	}
	
	return myWidth;
}

/* Function: Refresh Zoom */
function alwaysUpdateZoom(){
  if(touch == false){
    
    if($('.elevatezoom').length){
      
      var zoomImage = $('.elevatezoom .img-zoom');

      zoomImage.elevateZoom({
        gallery:'gallery_main', 
        galleryActiveClass: 'active', 
        zoomType: 'window',
        cursor: 'pointer',
        zoomWindowFadeIn: 300,
        zoomWindowFadeOut: 300,
        zoomWindowWidth: 300,
        zoomWindowHeight: 450,
        scrollZoom: true,
        easing : true,
        loadingIcon: '//cdn.shopify.com/s/files/1/0322/2265/t/4/assets/loader.gif?81071'
      });
      
      
        //pass the images to Fancybox
        $(".elevatezoom .img-zoom").bind("click", function(e) {  
          var ez =   $('.elevatezoom .img-zoom').data('elevateZoom');	
          $.fancybox(ez.getGalleryList(),{
            closeBtn  : false,
            helpers : {
              buttons	: {}
            }
          });
          return false;
        });
      
    }
    
  }
       // is touch
       else{
         
       }
}
      
// handle quickshop position
function positionQuickshop(){
  
  if(touch == false){
    var quickshops = $('.quick_shop');
    
    quickshops.each(function() {
      var parent = $(this).parents('.hoverBorder');
      $(this).css({
        'top': ((parent.height() / 2) - ($(this).outerHeight() / 2)) + 'px',
        'left': ((parent.outerWidth() / 2) - ($(this).outerWidth() / 2)) + 'px',
      });
    });
  }
}

   
// handle Animate
function handleAnimate(){
  if(touch == false){
    $('[data-animate]').each(function(){
      
      var $toAnimateElement = $(this);
      
      var toAnimateDelay = $(this).attr('data-delay');
      
      var toAnimateDelayTime = 0;
      
      if( toAnimateDelay ) { toAnimateDelayTime = Number( toAnimateDelay ); } else { toAnimateDelayTime = 200; }
      
      if( !$toAnimateElement.hasClass('animated') ) {
        
        $toAnimateElement.addClass('not-animated');
        
        var elementAnimation = $toAnimateElement.attr('data-animate');
        
        $toAnimateElement.appear(function () {
          
          setTimeout(function() {
            $toAnimateElement.removeClass('not-animated').addClass( elementAnimation + ' animated');
          }, toAnimateDelayTime);
          
        },{accX: 0, accY: -100},'easeInCubic');
        
      }
    });
  }
}
    
// handle scroll-to-top button
function handleScrollTop() {
  
  function totop_button(a) {
    var b = $("#scroll-to-top");
    b.removeClass("off on");
    if (a == "on") { b.addClass("on") } else { b.addClass("off") }
  }
  
  $(window).scroll(function() {
    var b = $(this).scrollTop();
    var c = $(this).height();
    if (b > 0) { 
      var d = b + c / 2;
    } 
    else { 
      var d = 1 ;
    }
    
    if (d < 1e3 && d < c) { 
      totop_button("off");
    } 
    else {
      
      totop_button("on"); 
    }
  });
  
  $("#scroll-to-top").click(function (e) {
    e.preventDefault();
    $('body,html').animate({scrollTop:0},800,'swing');
  });
}
      
function handleScrollTopCollection() {

  $("#scroll-to-top-collect").click(function (e) {
    e.preventDefault();
    $('body,html').animate({scrollTop:0},800,'swing');
  });
}
      
/* Function update scroll product thumbs */
function updateScrollThumbs(){
  if($('#gallery_main').length){
    
    if(touch){
      $('#product-image .main-image').on('click', function() {
        var _items = [];
        var _index = 0;
        var product_images = $('#product-image .image-thumb');
        product_images.each(function(key, val) {
          _items.push({'href' : val.href, 'title' : val.title});
          if($(this).hasClass('active')){
            _index = key;
          }
        });
        $.fancybox(_items,{
          closeBtn: false,
          index: _index,
          openEffect: 'none',
          closeEffect: 'none',
          nextEffect: 'none',
          prevEffect: 'none',
          helpers: {
            buttons: {}
          }
        });
        return false;
      });
    }
    else{
      
    }

    $('#product-image').on('click', '.image-thumb', function() {

      var $this = $(this);
      var background = $('.product-image .main-image .main-image-bg');
      var parent = $this.parents('.product-image-wrapper');
      var src_original = $this.attr('data-image-zoom');
      var src_display = $this.attr('data-image');
      
      background.show();
      
      parent.find('.image-thumb').removeClass('active');
      $this.addClass('active');
      
      parent.find('.main-image').find('img').attr('data-zoom-image', src_original);
      parent.find('.main-image').find('img').attr('src', src_display).load(function() {
        background.hide();
      });
      
      return false;
    });
  }
}
    
/* Function update scroll product thumbs */
function updateScrollThumbsQS(){
  if($('#gallery_main_qs').length){
    
    

    $('#quick-shop-image').on(clickEv, '.image-thumb', function() {

      var $this = $(this);
      var background = $('.product-image .main-image .main-image-bg');
      var parent = $this.parents('.product-image-wrapper');
      var src_original = $this.attr('data-image-zoom');
      var src_display = $this.attr('data-image');
      
      background.show();
      
      parent.find('.image-thumb').removeClass('active');
      $this.addClass('active');
      
      parent.find('.main-image').find('img').attr('data-zoom-image', src_original);
      parent.find('.main-image').find('img').attr('src', src_display).load(function() {
        background.hide();
      });
      
      return false;
    });
  }
}
    
/* Handle Carousel */
function handleCarousel(){
  /*Handle collection slide*/
  if($('#collect-slider').length){
    $('#collect-slider').responsiveSlider({
      autoplay: true,
      interval: 5000,
      transitionTime: 300
   });
  }
  
  /* Handle main slideshow */
  if($('#home-slider').length){
    $('#home-slider').responsiveSlider({
      autoplay: false,
      interval: 5000,
      transitionTime: 300
    });
  }
  
  
  /* Handle Banners */
  if($('#home_banners').length){
    imagesLoaded('#home_banners', function() {
      $("#home_banners").owlCarousel({
        navigation : true,
        pagination: false,
        items: 4,
        itemsDesktop : [1199,4],
        itemsDesktopSmall : [979,4],
        itemsTablet: [768,3],
        itemsTabletSmall: [540,2],
        itemsMobile : [360,1],
        scrollPerPage: true,
        navigationText: ['<i class="fa fa-caret-left btooltip" title="Previous"></i>', '<i class="fa fa-caret-right btooltip" title="Next"></i>'],
        beforeMove: function(elem) {
          if(touch == false){
            var items = elem.find('.not-animated');
            items.removeClass('not-animated').unbind('appear');
          }
        },
        afterInit: function(elem){
          elem.find('.btooltip').tooltip();
        }
      });
    });
  }
  
  /* Handle Featured Collections */
  if($("#home_collections").length){
    imagesLoaded('#home_collections', function() {
      $("#home_collections").owlCarousel({
        navigation : true,
        pagination: false,
        items: 4,
        itemsDesktop : [1199,4],
        itemsDesktopSmall : [979,4],
        itemsTablet: [768,3],
        itemsTabletSmall: [540,2],
        itemsMobile : [360,1],
        scrollPerPage: true,
        navigationText: ['<i class="fa fa-caret-left btooltip" title="Previous"></i>', '<i class="fa fa-caret-right btooltip" title="Next"></i>'],
        beforeMove: function(elem) {
          if(touch == false){
            var items = elem.find('.not-animated');
            items.removeClass('not-animated').unbind('appear');
          }
        },
        afterInit: function(elem){
          elem.find('.btooltip').tooltip();
        }
      });
    });
  }
  
  /* Handle Partners Logo */
  if($('#partners').length){
    imagesLoaded('#partners', function() {
      $("#partners").owlCarousel({
        navigation : true,
        pagination: false,
        items: 7,
        itemsDesktop : [1199,6],
        itemsDesktopSmall : [979,5],
        itemsTablet: [768,4],
        itemsTabletSmall: [540,2],
        itemsMobile : [360,1],
        scrollPerPage: true,
        navigationText: ['<i class="partner-prev btooltip" title="Previous"></i>', '<i class="partner-next btooltip" title="Next"></i>'],
        beforeMove: function(elem) {
          if(touch == false){
            var items = elem.find('.not-animated');
            items.removeClass('not-animated').unbind('appear');
          }
        },
        afterInit: function(elem){
          elem.find('.btooltip').tooltip();
        }
      });
    });
  }
   
  /* Handle product thumbs */
  if($("#gallery_main").length){
    $("#gallery_main").owlCarousel({
      navigation : true,
      pagination: false,
      items: 5,
      itemsDesktop : [1199,3],
      itemsDesktopSmall : [979,3],
      itemsTablet: [768,3],
      itemsMobile : [479,3],
      scrollPerPage: true,
      navigationText: ['<i class="fa fa-caret-left btooltip" title="Previous"></i>', '<i class="fa fa-caret-right btooltip" title="Next"></i>'],
      afterInit: function(elem){
        elem.find('.btooltip').tooltip();
      }
    });
  }
   
  /* Handle related products */
  if($("#prod-related").length){
    $("#prod-related").owlCarousel({
      navigation : true,
      pagination: false,
      items: 5,
      itemsDesktop : [1199,3],
      itemsDesktopSmall : [979,3],
      itemsTablet: [768,2],
      itemsTabletSmall: [540,2],
      itemsMobile : [360,1],
      scrollPerPage: true,
      navigationText: ['<i class="fa fa-chevron-left btooltip" title="Previous"></i>', '<i class="fa fa-chevron-right btooltip" title="Next"></i>'],
      beforeMove: function(elem) {
        if(touch == false){
          var items = elem.find('.not-animated');
          items.removeClass('not-animated').unbind('appear');
        }
      },
      afterUpdate: function() {
        positionQuickshop();
      },
      afterInit: function(elem){
        elem.find('.btooltip').tooltip();
      }
    });
  }
}

/* Handle search box on mobile */
function callbackSearchMobile(){
  var button = $('.is-mobile .is-mobile-search i');
  var form = $('.is-mobile .is-mobile-search .search-form');
  button.mouseup(function(search) {
    form.show();
  });
  form.mouseup(function() { 
    return false;
  });
  $(this).mouseup(function(search) {
    if(!($(search.target).parent('.is-mobile .is-mobile-search').length > 0)) {
      form.hide();
    }
  });  
}
/* Handle search box on pc */
function handleBoxSearch(){
  if($('#header-search input').length){
    $('#header-search input').focus(function() {
      $(this).parent().addClass('focus');
    }).blur(function() {
      $(this).parent().removeClass('focus');
    });
  }
}
    
/* Handle login box */
function handleBoxLogin(){
  $("#loginBox input").focus(function() {
    $(this).parents('#loginBox').addClass('focus');
  }).blur(function() {
    $(this).parents('#loginBox').removeClass('focus');
  });
}

    
/* Handle Map with GMap */
function handleMap(){
  if(jQuery().gMap){
    if($('#contact_map').length){
      $('#contact_map').gMap({
        zoom: 17,
        scrollwheel: false,
        maptype: 'ROADMAP',
        markers:[
          {
            address: '249 Ung Văn Khiêm, phường 25, Ho Chi Minh City, Vietnam',
            html: '_address'
          }
        ]
      });
    }
  }
}
    
/* Handle Grid - List */
function handleGridList(){
  if($('#goList').length){
    $('#goList').on(clickEv, function(e){
      $(this).parent().find('li').removeClass('active');
      $(this).addClass('active');
      
      $('#sandBox .element').addClass('full_width');
      $('#sandBox .element .row-left').addClass('col-md-5');
      $('#sandBox .element .row-right').addClass('col-md-19');
      
      if(clickEv == 'touchstart'){
        $(this).click();
        return true;
      }
      
      /* re-call handle position */
      positionQuickshop();
    });
  }
  
  if($('#goGrid').length){
    $('#goGrid').on(clickEv, function(e){
      $(this).parent().find('li').removeClass('active');
      $(this).addClass('active');
      
      $('#sandBox .element').removeClass('full_width');
      $('#sandBox .element .row-left').removeClass('col-md-5');
      $('#sandBox .element .row-right').removeClass('col-md-19');
      
      if(clickEv == 'touchstart'){
        $(this).click();
        return true;
      }
      
      /* re-call handle position */
      positionQuickshop();
    });
  }
}

/* Handle detect platform */
function handleDetectPlatform(){
  /* DETECT PLATFORM */
  $.support.touch = 'ontouchend' in document;
  
  if ($.support.touch) {
    touch = true;
    $('body').addClass('touch');
    clickEv = 'touchstart';
  }
  else{
    $('body').addClass('notouch');
    if (navigator.appVersion.indexOf("Mac")!=-1){
      if (navigator.userAgent.indexOf("Safari") > -1){
        $('body').addClass('macos');
      }
      else if (navigator.userAgent.indexOf("Chrome") > -1){
        $('body').addClass('macos-chrome');
      }
        else if (navigator.userAgent.indexOf("Mozilla") > -1){
          $('body').addClass('macos-mozilla');
        }
    }
  }
}
    
/* Handle tooltip */
function handleToolTip(){
  if(touch == false){
    if($('.btooltip').length){
      $('.btooltip').tooltip();
    }
  }
}
    
/* Handle product quantity */
function handleQuantity(){
  if($('.quantity-wrapper').length){
    $('.quantity-wrapper').on(clickEv, '.qty-up', function() {
      var $this = $(this);
      
      var qty = $this.data('src');
      $(qty).val(parseInt($(qty).val()) + 1);
    });
    $('.quantity-wrapper').on(clickEv, '.qty-down', function() {
      var $this = $(this);
      
      var qty = $this.data('src');
      
      if(parseInt($(qty).val()) > 1)
        $(qty).val(parseInt($(qty).val()) - 1);
    });
  }
}
    
/* Handle sidebar */
function handleSidebar(){
  /* Add class first, last in sidebar */
  if($('.sidebar').length){
    $('.sidebar').children('.row-fluid').first().addClass('first');
    $('.sidebar').children('.row-fluid').last().addClass('last');
  }
}
    
/* Handle sort by */
function handleSortBy(){
  if($('#sortForm li.sort').length){
    $('#sortForm li.sort').click(function(){
      
      var button = $('#sortButton');
      var box = $('#sortBox');
      
      $('#sortButton .name').html($(this).html());
      
      button.removeClass('active');
      box.hide();
    });
  }
}
    
/* Handle dropdown box */
function handleDropdown(){
  if($(".dropdown-toggle").length){
    $(".dropdown-toggle").parent().hover(function (){
      if(touch == false && getWidthBrowser() > 768 ){
        $(this).find('.dropdown-menu').stop(true, true).slideDown(300);
      }
    }, function (){
      if(touch == false && getWidthBrowser() > 768 ){
        $(this).find('.dropdown-menu').hide();
      }
    });
  }
  $('.dropdown').on('click', function() {
      if(touch == false && getWidthBrowser() > 768 ){
        var href = $(this).find('.link-dropdown').attr('href');
        window.location = href;
    }
  });
  $('.cart-link').on('click', function() {
      if(touch == false && getWidthBrowser() > 768 ){
        var href = $(this).find('.link-dropdown').attr('href');
        window.location = href;
    }
  });
}
    
/* Handle collection tags */
function handleCollectionTags(){
  if($('#collection_tags').length){
    $('#collection_tags').on('change', function() {
      window.location = $(this).val();
    });
  }
}
   
/* Handle menu with scroll*/
function handleMenuScroll(){
  var scrollTop = $(this).scrollTop();
  var heightHeader = $('#top').outerHeight() + $('#navigation').outerHeight();
  var heightNav = $('#navigation').outerHeight();
  
  if(touch == false && getWidthBrowser() >= 1024){
    if(scrollTop > heightHeader){
      if(!$('#navigation').hasClass('on')){
        $('<div style="min-height:'+heightNav+'px"></div>').insertBefore('#navigation');
        $('#navigation').addClass('on').addClass('animated');
      }
    }
    else{
      if($('#navigation').hasClass('on')){
        $('#navigation').prev().remove();
        $('#navigation').removeClass('on').removeClass('animated');
      }
    }
  }
}

/* Handle when window resize */
$(window).resize(function() {

  /* re-call position quickshop */
  positionQuickshop();
  
  /* reset menu with condition */
  if(touch == true || getWidthBrowser() < 1024){
    $('#navigation').removeClass('on');
  }
});

/* Handle Quickshop */
function handleQuickshop(){
	$('body').on('click','.quick_shop',function(e){
		var action = $(this).attr('data-href');
		var target = $(this).attr('data-target')
		$(target).load(action, function() {
			$('#top').addClass('z-idx');
			
			$('.btooltip').tooltip();
			var zoomImage = $('.elevatezoom_qs .img-zoom');
			
			zoomImage.elevateZoom({
			  gallery:'gallery_main_qs', 
			  galleryActiveClass: 'active', 
			  zoomType: 'window',
			  cursor: 'pointer',
			  zoomWindowFadeIn: 300,
			  zoomWindowFadeOut: 300,
			  zoomWindowWidth: 250,
			  zoomWindowHeight: 350,
			  scrollZoom: true,
			  easing : true,
			  loadingIcon: './assets/images/loader.gif'
			});
			
			//pass the images to Fancybox
			$(".elevatezoom_qs .img-zoom").bind("click", function(e) {  
			  var ez =   $('.elevatezoom_qs .img-zoom').data('elevateZoom');	
			  $.fancybox(ez.getGalleryList(),{
				closeBtn  : false,
				helpers : {
				  buttons	: {}
				}
			  });
			  return false;
			});
			
			$("#gallery_main_qs").show().owlCarousel({
			  navigation : true,
			  pagination: false,
			  items: 3,
			  itemsDesktop : [1199,3],
			  itemsDesktopSmall : [979,3],
			  itemsTablet: [768,3],
			  itemsMobile : [479,3],
			  scrollPerPage: true,
			  navigationText: ['<i class="fa fa-angle-left" title="Previous"></i>', '<i class="fa fa-angle-right" title="Next"></i>']
			});
		});
		
		e.preventDefault();
	});
  
	$('body').on('hidden.bs.modal', '.modal', function() {
		$(this).empty();
		$('.zoomContainer').css('z-index', '1');
		$('#top').removeClass('z-idx');
	});
}

/* Handle when window scroll */
$(window).scroll(function() {
  /* re-call handle menu when scroll */
  handleMenuScroll();
});

/* handle when window loaded */
$(window).load(function() {

  /* re-call position quickshop */
  positionQuickshop();
});

jQuery(document).ready(function($) {
  
  /* DETECT PLATFORM */
  handleDetectPlatform();
  
  /* Handle Animate */
  handleAnimate();
  
  /* Handle Carousel */
  handleCarousel();
  
  /* Handle BoxLogin */
  handleBoxLogin()
  
  /* Handle search box on pc */
  handleBoxSearch();
  
  /* Handle search box on mobile */
  callbackSearchMobile();
  
  /* Handle position quickshop */
  positionQuickshop();

  /* Handle scroll to top */
  handleScrollTop();
  handleScrollTopCollection();
  
  /* Handle dropdown box */
  handleDropdown();
  
  /* handle menu when scroll */
  handleMenuScroll();
  
  /* Handle tooltip */
  handleToolTip();
  
  /* Handle Map with GMap */
  handleMap();
  
  /* Handle Quickshop */
  handleQuickshop();
  
  /* Handle sidebar */
  handleSidebar();
  
  /* Handle zoom for product image */
  alwaysUpdateZoom();
  
  /* Handle quantity */
  handleQuantity();
  
  /* Handle product thumbs */
  if(touch){
    updateScrollThumbs();
  }
  else{
    
  }

  /* Handle sort by */
  handleSortBy();
     
  /* Handle grid - list */
  handleGridList();
  
  /* Handle collection tags */ 
  handleCollectionTags();
     
  $('.dropdown-menu').on(clickEv, function (e) {
    e.stopPropagation();
  });
  $('.dropdown-menu').on('click', function (e) {
    e.stopPropagation();
  });
  $('.btn-navbar').on('click', function() {
    return true;
  });
   
  
});