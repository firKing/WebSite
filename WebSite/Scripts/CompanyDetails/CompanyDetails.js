$(document).ready(function() {
    $("button.btn-success").click(function() {
        var teamId = $("button.btn-success").attr("value");
        $.ajax({
            type: "POST",
            url: "AddTeam",
            dataType: "json",
            data: { teamId: teamId },
            success: function(data) {
                if (data) {
                    alert("加入成功！");
                } else {
                    alert("加入失败");
                }
            }
        });
    });
});