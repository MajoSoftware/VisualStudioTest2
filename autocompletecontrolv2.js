// Function to initialize 1 autocomplete control (can be used for all autocomplete controls on a page)
function initAutoCompleteControl(autocompleteTextboxId, autocompleteHiddenFieldId, autocompleteDataUrl) {

    // Init the autocomplete textbox
    $('#' + autocompleteTextboxId).autocomplete({

        source: function (request, response)                    // Retrieve data event
        {
            // Determine Url to dataSource, incl. search term (handler, WebApi or something) to fill the items list
            var url = autocompleteDataUrl + request.term;

            // Get the data with Ajax call
            $.ajax({
                url: url,
                type: "GET",
                contentType: "application/json; charset=utf-8",
                success: function (data) {

                    // Return a list with items containing a 'label' and 'val' property
                    var resultData = $.map(data, function (item) {
                        return {
                            label: item.Text,
                            value: item.Value
                        }
                    });

                    // Return the resultData (fill list)
                    response(resultData);
                },
                error: function (response) {
                    alert('autocomplete data error: ' + response.responseText);
                },
                failure: function (response) {
                    alert('autocomplete data failure: ' + response.responseText);
                }
            });

        },

        select: function (e, ui) {                  // select item event
            e.preventDefault()                      // <--- Prevent the value from being inserted into the textbox (default behaviour).

            $('#' + autocompleteHiddenFieldId).val(ui.item.value);  // set hidden field value
            $(this).val(ui.item.label);             // set textbox text
        },
        focus: function (event, ui) {               // keyboard arrow up/down event
            event.preventDefault();                 // Prevent the default focus behavior.

            $('#' + autocompleteHiddenFieldId).val(ui.item.value);  // set hidden field value
            $(this).val(ui.item.label);             // set textbox text                    
        }

    });

}
