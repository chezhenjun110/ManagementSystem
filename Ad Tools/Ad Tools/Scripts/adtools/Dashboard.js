

    $("#Logfile").on("change", function () {
      
          
            var logname = $("#Logfile").val();
            $.ajax({
                type: "post",
                url: "/Dashboard/GetLog",
                data: "logname=" + logname,
                dataType: "json",
                success: function (data) {
                    $("#tablespace").html(data.Message);
                    $('#example').dataTable({
                        "order": [[5, "desc"]]
                    });
                },
                error: function () {
                    $("#message").html("<h5 style=\"color:red\">Network interrupt, networking timeout</h5>");
                }

            });

        

    });
