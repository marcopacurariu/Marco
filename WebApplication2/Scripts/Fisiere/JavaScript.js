$(document).ready(function () {
    var updateResources = function () {
        updateResource("Clay");
        updateResource("Wheat");
        updateResource("Iron");
        updateResource("Wood");
    };
    var updateResource = function (resourceName) {
        var start = new Date();
        var currentProduction = 0;
        var currentValue = parseFloat($(".res-value." + resourceName).text());
        var lastupdate = Date.parse($(".res-update." + resourceName).text());
   //     console.log(currentValue);
     //   console.log(lastupdate);
        var mines = $(".mines").find("." + resourceName);
        console.log($(".mines").find("." + resourceName))
        $.each(mines, function (index, value) {
            //console.log($(value).find(".hourProduction").text())
            currentProduction += parseInt($(value).find(".hourProduction").text());

            //console.log(currentProduction);
        });
        //console.log(currentProduction)
        var nextValue = (currentValue + ((start.getTime() - lastupdate) / 1000 / 60 / 60) * currentProduction).toFixed(4);
       // console.log((start.getTime() - lastupdate) / 1000 / 60 / 60)
        $(".res-value." + resourceName).text(nextValue);

        $(".res-update." + resourceName).text(start.strftime("%Y-%m-%d %H:%M:%S"));

    };
    setInterval(updateResources, 500);
}
);