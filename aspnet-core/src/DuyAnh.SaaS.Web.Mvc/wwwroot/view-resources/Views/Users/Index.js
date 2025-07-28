(function ($) {

  var _userService = abp.services.app.user,
    l = abp.localization.getSource("SaaS"),
    _$modal = $("#UserCreateModal"),
    _$form = _$modal.find("form"),
    _$table = $("#UsersTable");

  var _createModal = new app.ModalManager({
    viewUrl: abp.appPath + 'Users/CreateModal',
    scriptUrl: abp.appPath + 'view-resources/Views/Users/_CreateModal.js',
    modalClass: 'UsersCreateModal',
    modalType: 'modal-xl'
  });

  var _editModal = new app.ModalManager({
    viewUrl: abp.appPath + 'Users/EditModal',
    scriptUrl: abp.appPath + 'view-resources/Views/Users/_EditModal.js',
    modalClass: 'UsersEditModal',
    modalType: 'modal-xl'
  })

  $("#UserCreateModal").on("click", function () {
    _createModal.open();
  });
  $(document).on("click", "#btn-edit", function () {
    var id = $(this).attr("data-user-id");
    _editModal.open({ userId: id });
  });

  var _$usersTable = _$table.DataTable({
    paging: true,
    serverSide: true,
    processing: true,
    listAction: {
      ajaxFunction: _userService.getAll,
      inputFilter: function () {
        return $("#UsersSearchForm").serializeFormToObject(true);
      },
    },
    buttons: [
      {
        name: "refresh",
        text: '<i class="fas fa-redo-alt"></i>',
        action: () => _$usersTable.draw(false),
      },
    ],
    responsive: {
      details: {
        type: "column",
      },
    },
    columnDefs: [
      {
        targets: 0,
        className: "control",
        defaultContent: "",
        orderable: false,
      },
      {
        targets: 1,
        data: "userName",
      },
      {
        targets: 2,
        data: "fullName",
        orderable: false,
      },
      {
        targets: 3,
        data: "emailAddress",
      },
      {
        targets: 4,
        data: "isActive",
        orderable: false,
        render: (data) =>
          `<input type="checkbox" disabled ${data ? "checked" : ""}>`,
      },
      {
        targets: 5,
        data: null,
        orderable: false,
        autoWidth: false,
        defaultContent: "",
        render: (data, type, row, meta) => {
          return `
             <div class="dropdown">
              <button class="btn btn-primary btn-sm dropdown-toggle" type="button" id="dropdownMenuButton-${row.id}" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                Actions
              </button>
              <div class="dropdown-menu" aria-labelledby="dropdownMenuButton-${row.id}">
                <a class="dropdown-item btn-edit edit-user" id="btn-edit" href="#" data-user-id="${row.id}">Edit</a>
                <a class="dropdown-item btn-delete delete-user" href="#" data-user-id="${row.id}" data-user-name="${row.name}">Delete</a>
              </div>
            </div>
          `;
        },
      },
    ],
  });

  _$form.validate({
    rules: {
      Password: "required",
      ConfirmPassword: {
        equalTo: "#Password",
      },
    },
  });



  $(document).on("click", ".delete-user", function () {
    var userId = $(this).attr("data-user-id");
    var userName = $(this).attr("data-user-name");

    deleteUser(userId, userName);
  });

  function deleteUser(userId, userName) {
    abp.message.confirm(
      abp.utils.formatString(l("AreYouSureWantToDelete"), userName),
      null,
      (isConfirmed) => {
        if (isConfirmed) {
          _userService
            .delete({
              id: userId,
            })
            .done(() => {
              abp.notify.info(l("SuccessfullyDeleted"));
              _$usersTable.ajax.reload();
            });
        }
      },
    );
  }

  $(document).on("click", ".edit-user", function (e) {
    var userId = $(this).attr("data-user-id");

    e.preventDefault();
    abp.ajax({
      url: abp.appPath + "Users/EditModal?userId=" + userId,
      type: "POST",
      dataType: "html",
      success: function (content) {
        $("#UserEditModal div.modal-content").html(content);
      },
      error: function (e) {},
    });
  });

  $(document).on("click", 'a[data-bs-target="#UserCreateModal"]', (e) => {
    $('.nav-tabs a[href="#user-details"]').tab("show");
  });

  abp.event.on("user.edited", (data) => {
    _$usersTable.ajax.reload();
  });

  _$modal
    .on("shown.bs.modal", () => {
      _$modal.find("input:not([type=hidden]):first").focus();
    })
    .on("hidden.bs.modal", () => {
      _$form.clearForm();
    });

  $(".btn-search").on("click", (e) => {
    _$usersTable.ajax.reload();
  });

  $(".txt-search").on("keypress", (e) => {
    if (e.which == 13) {
      _$usersTable.ajax.reload();
      return false;
    }
  });
})(jQuery);
