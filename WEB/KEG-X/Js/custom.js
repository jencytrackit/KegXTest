$(function(){
			$('html').addClass('html-is-enable-in-this-site');
			$('.sub-menu').hide();
			$('.acrodian li').each(function(){
				if($(this).children('ul').length > 0){				
				$(this).addClass('arrow')
				}	
			}); 
			$('li.arrow').click(function(){						
						if($(this).children('ul').is(':visible')){
						$(this).removeClass('active').css('background','url(images/arrow1.png) no-repeat  180px 0')
						$(this).children('ul').hide(500);						
					}
				else {
					$('.sub-menu').hide(200);
					$('li.arrow').removeClass('active');
					$(this).addClass('active').css('background','url(images/arrow.png) no-repeat  180px 7px')
					$(this).children('.sub-menu').show(500);
					return false;
					}					
			});
			$('select').each(function() {

   			 // Cache the number of options
			var $this = $(this),
				numberOfOptions = $(this).children('option').length;
		
			// Hides the select element
			$this.addClass('s-hidden');
		
			// Wrap the select element in a div
			$this.wrap('<div class="select"></div>');
		
			// Insert a styled div to sit over the top of the hidden select element
			$this.after('<div class="styledSelect"></div>');
		
			// Cache the styled div
			var $styledSelect = $this.next('div.styledSelect');
		
			// Show the first select option in the styled div
			$styledSelect.text($this.children('option').eq(0).text());
		
			// Insert an unordered list after the styled div and also cache the list
			var $list = $('<ul />', {
				'class': 'options'
			}).insertAfter($styledSelect);
		
			// Insert a list item into the unordered list for each select option
			for (var i = 0; i < numberOfOptions; i++) {
				$('<li />', {
					text: $this.children('option').eq(i).text(),
					rel: $this.children('option').eq(i).val()
				}).appendTo($list);
			}
		
			// Cache the list items
			var $listItems = $list.children('li');
		
			// Show the unordered list when the styled div is clicked (also hides it if the div is clicked again)
			$styledSelect.click(function(e) {
				e.stopPropagation();
				$('div.styledSelect.active').each(function() {
					$(this).removeClass('active').next('ul.options').hide();
				});
				$(this).next('ul.options').toggle();
			});
		
			// Hides the unordered list when a list item is clicked and updates the styled div to show the selected list item
			// Updates the select element to have the value of the equivalent option
			$listItems.click(function(e) {
				e.stopPropagation();
				$styledSelect.text($(this).text()).removeClass('active');
				$this.val($(this).attr('rel'));
				$list.hide();
				/* alert($this.val()); Uncomment this for demonstration! */
			});
		
			// Hides the unordered list when clicking outside of it
			$(document).click(function() {
				$styledSelect.removeClass('active');
				$list.hide();
			});
		
		});
			$('.click').click(function(){
				if($(this).hasClass('submit')){
						return false;
					}
					else {
						$('.click-open').click();
					}
			});
			$('.click-open').change(function () {
				 var path = $(this).val();
				$('.takepath').val(path);
				$('.changebtn').val('Upload').attr("type",'submit').addClass('submit');
			});
			
			var leftpanelheight = $('.contentnfooter').outerHeight();
				newheight = leftpanelheight-74
			//$('.left-panel').css('min-height',newheight)
			
			
			$('.hide-show').click(function() {
				$('.body-wrapper').toggleClass('body-wrapper-fullsize');
				$('.LeftMenuDevider').toggleClass('light-border');
				var $sliderMenu = $('.left-panel');
				var $sliderPanel = $('.contentnfooter');
				$sliderMenu.animate({
				  left: parseInt($sliderMenu.css('left'),10) == -225 ?
				   0 : -225
				});
				$sliderPanel.animate({
			
				   left: parseInt($sliderPanel.css('left'),10) == -225 ?
				   0 : -225
				});	
			  });

			
		});// JavaScript Document