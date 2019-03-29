var UserInfo = {};
var UserId = "";

var Coffee = {
    GetAllCoffeLovers: function () {

        $.ajax({
            type: "GET",

            url: "api/coffelovers",
            contentType: "application/json",
            success: function (data) {

                if (!data.Success) {
                    alert(data.Result);
                }
                else {
                    $("#Main").empty();

                    $(data.Result.CoffeLovers).each(function () {
                        $("#Main").append("<div class='clDiv " + (this.PinExist ? "secure" : "") + "' data-id='" + this.Id + "' style=';background:url(data:image/jpeg;base64," + this.Photo + ");background-size: 100%; '>" + this.Name.split(' ')[0] + "<br>" + this.Name.split(' ')[1] + "<br><br>" + this.Count + "</div>");

                    });

                    $("#TimeBox").html(data.Result.DateTime);

                    // how many milliseconds is a long press?
                    var longpress = 2000;
                    // holds the start time
                    var start = 0;
                    var timer;
                    var timer2;
                    var dblClik = 0;

                    jQuery(".clDiv").on('mousedown', function (e) {
                        if (dblClik != 1 && start != 0)
                        { dblClik = 1; }
                        UserId = $(this).attr("data-id");
                        start = new Date().getTime();
                        timer = setTimeout(
                            function () {
                                Coffee.DellCoffeeToUser();
                            }, longpress);
                    });

                    jQuery(".clDiv").on('mouseleave', function (e) {
                        start = 0;
                        clearTimeout(timer);
                    });

                    jQuery(".clDiv").on('mouseup', function (e) {
                        UserId = $(this).attr("data-id");
                        if (new Date().getTime() <= (start + longpress)) {
                            clearTimeout(timer);

                            if (dblClik == 1) {
                                clearTimeout(timer2);
                                alert("dblClick");
                                dblClik = 0;
                            }
                            else {
                                timer2 = setTimeout(
                                        function () {
                                            Coffee.Pin(Coffee.AddCoffeeToUser);
                                        }, 200);
                            }

                        }
                    });

                }

            }

        });

    },
    AddCoffeeLover: function (pin) {
        var User = UserInfo;
        User.PinHash = pin;

        if (jQuery.isEmptyObject(User)) {
            alert("Пользователь не выбран");
        }
        else {
            $.ajax({
                type: "POST",
                data: JSON.stringify(User),
                url: "api/coffelovers",
                contentType: "application/json",
                success: function (data) {

                    if (!data.Success) {
                        alert(data.Result);
                    }
                    else {

                        Coffee.GetAllCoffeLovers();
                    }

                    $("#photo").attr("src", "");
                    $("input[name=username]").val("");
                    $("#photo").css("background", '');
                    $("#photo").hide();

                }

            });
        }

    },
    AddCoffeeToUser: function (pin) {

        var uc = {
            id: UserId,
            pin: pin
        }

        $.ajax({
            type: "POST",
            url: "api/AddCoffee/",
            data: JSON.stringify(uc),
            contentType: "application/json",
            success: function (data) {

                if (!data.Success) {
                    Coffee.Pin(Coffee.AddCoffeeToUser)
                    alert(data.Result);

                }
                else {
                    Coffee.GetAllCoffeLovers();
                }

            }

        });

    },
    DellCoffeeToUser: function (pin) {
        var uc = {
            id: UserId,
            pin: pin
        }


        $.ajax({
            type: "POST",
            url: "api/DellCoffee/",
            data: JSON.stringify(uc),
            contentType: "application/json",
            success: function (data) {

                if (!data.Success) {
                    alert(data.Result);
                }
                else {
                    Coffee.GetAllCoffeLovers();
                }

            }

        });

    },
    Pin: function (func) {

        $(".pad").off();
        $(".pad").click(function () {
            var curentVal = $(this).html();
            var tb = $(".passTb");
            switch (curentVal) {
                case '<<':
                case '&lt;&lt;':
                    if (tb.val().length == 0) {
                        $('#PinPadArea').hide();
                    }
                    tb.val(tb.val().substr(1, tb.val().length - 1))
                    break;
                case '>>':
                case '&gt;&gt;':
                    var res = "";
                    if (tb.val().length != 0)
                        res = Coffee.hash(tb.val());
                    tb.val("");
                    func(res);
                    $('#PinPadArea').hide();
                    break;
                default:
                    tb.val(tb.val() + '' + curentVal);
                    break;
            }
        });

        $('#PinPadArea').show();

    },
    InitCombo: function(){
        $('input[name=username]').typeahead({
            //источник данных
            source: function (query, process) {
                if (query.length > 2)
                    return $.get('api/getGal/' + query,
                          function (response) {
                              var data = new Array();

                              $.each(response, function () {

                                  data.push(this.EMail + '_' + this.DisplayName + '(' + this.EMail + ')');
                              })
                              return process(data);
                          },
                          'json'
                          );
            }
            //источник данных
            //вывод данных в выпадающем списке
   , highlighter: function (item) {
       var parts = item.split('_');
       parts.shift();
       return parts.join('_');
   }
            //вывод данных в выпадающем списке
            //действие, выполняемое при выборе елемента из списка
   , updater: function (item) {
       var parts = item.split('_');
       var userId = parts.shift();

       var data = { email: userId };
       $.ajax({
           type: "POST",
           data: JSON.stringify(data),
           url: "api/getgal",
           contentType: "application/json",
           success: function (data) {
               var ur = data;

               UserInfo.Name = ur.DisplayName;
               UserInfo.Photo = ur.Photo;
               UserInfo.Email = ur.EMail;

               $("#photo").css("background", 'url(data:image/jpeg;base64,' + ur.Photo + ') no-repeat');
               $("#photo").css("background-size", '100%');
               $("#photo").show();


           }

       });

       return parts.join('_');
   }
            //действие, выполняемое при выборе елемента из списка
        });

},
    hash: function(str) {
    var hash = 0;
    if (str.length == 0) return hash;
    for (i = 0; i < str.length; i++) {
        char = str.charCodeAt(i); hash = ((hash << 5) - hash) + char; hash = hash & hash; // Convert to 32bit integer
    }
    return hash;
}
}





