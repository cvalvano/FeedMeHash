$(document).ready(function () {
    
    $("#btnSubmit").click(function () {
        $("#divTweets").empty();

        $.ajax({
            url: "/Home/Search",
            data: { "hashText": $("#txtHash").attr("value") },
            type: "POST",
            dataType: 'json',
            async: false
        })
        .done(function (data) {
            var source = $("#tweetTemplate").html();
            var template = Handlebars.compile(source);
            var testTweet = [{ created_at: "test" }];
            var testJson = jQuery.parseJSON(data);
            var tweetData = { tweets: testJson };

            console.log(tweetData);

            $("#divTweets").append(template(tweetData));

            $("#divTweets").removeAttr("hidden");

        })
        .fail(function (data) {
            alert(data);
        });
    });

});