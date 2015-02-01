$(document).ready(function() {
    $("button.btn-success").click(function() {
        var teamId = $("button.btn-success").attr("value");
        $.ajax({
            type: "POST",
            url: "AddTeam",
            dataType: "json",
            data: { teamId: teamId },
            success: function(data) {
                console.log(data);
            }
        });
    });
});