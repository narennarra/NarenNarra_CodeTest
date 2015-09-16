function converttoWords() {
    if ($("#Amount").val() != '') {
        $.ajax({
            url: 'Home/Convert',
            type: 'GET',
            cache: false,
            data: { 'Amount': $("#Amount").val() },
            success: function (words) {
                $("#output").html(words)
                return words;
            },
            error: function (error) {
                $("#output").html("Error Occured" + error);
            }
        });
    }
    else { $("#output").html("Please enter a number."); }
}