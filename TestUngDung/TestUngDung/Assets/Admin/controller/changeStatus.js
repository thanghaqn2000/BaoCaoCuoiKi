var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/User/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.text('Active');
                        btn.removeClass('btn-danger');
                        btn.addClass('btn-info');
                    }
                    else {
                        btn.text('Blocked');
                        btn.removeClass('btn-info');
                        btn.addClass('btn-danger');

                    }
                }
            });
        });
    }
}
user.init();