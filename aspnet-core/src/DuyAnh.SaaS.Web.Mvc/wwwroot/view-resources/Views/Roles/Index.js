(function ($) {
  var _roleService = abp.services.app.role,
    l = abp.localization.getSource("SaaS"),
    _$modal = $("#RoleCreateModal"),
    _$form = _$modal.find("form"),
    _$table = $("#RolesTable");


  var _createModal = new app.ModalManager({
    viewUrl: abp.appPath + 'Roles/Create',
    scriptUrl: abp.appPath + 'view-resources/Views/Roles/_CreateModal.js',
    modalClass: 'RolesCreateModal',
    modalType: 'modal-xl'
  });

  var _editModal = new app.ModalManager({
    viewUrl: abp.appPath + 'Roles/Edit',
    scriptUrl: abp.appPath + 'view-resources/Views/Roles/_EditModal.js',
    modalClass: 'RolesEditModal',
    modalType: 'modal-xl'
  })

  $("#BtnCreateRoleModal").on("click", function () {
    _createModal.open();
  });
  $(document).on("click", ".btn-edit", function (e) {
    e.preventDefault(); // ngăn chuyển hướng nếu là <a href="#">
    var idRole = $(this).attr("data-role-id");
    _editModal.open({ roleId: idRole });
  });

  var _$rolesTable = _$table.DataTable({
    paging: true,
    serverSide: true,
    processing: true,
    listAction: {
      ajaxFunction: _roleService.getAll,
      inputFilter: function () {
        return $("#RolesSearchForm").serializeFormToObject(true);
      },
    },
    buttons: [
      {
        name: "refresh",
        text: '<i class="fas fa-redo-alt"></i>',
        action: () => _$rolesTable.draw(false),
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
        data: "name",
      },
      {
        targets: 2,
        data: "displayName",
      },
      {
        targets: 3,
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
                <a class="dropdown-item btn-edit edit-role" id="btn-edit" href="#" data-role-id="${row.id}">Edit</a>
                <a class="dropdown-item btn-delete delete-role" href="#" data-role-id="${row.id}" data-role-name="${row.name}">Delete</a>
              </div>
            </div>
          `;
        },
      },
    ],
  });

  _$form.find(".save-button").on("click", (e) => {
    e.preventDefault();

    if (!_$form.valid()) {
      return;
    }

    var role = _$form.serializeFormToObject();
    role.grantedPermissions = [];
    var _$permissionCheckboxes = _$form[0].querySelectorAll(
      "input[name='permission']:checked",
    );
    if (_$permissionCheckboxes) {
      for (
        var permissionIndex = 0;
        permissionIndex < _$permissionCheckboxes.length;
        permissionIndex++
      ) {
        var _$permissionCheckbox = $(_$permissionCheckboxes[permissionIndex]);
        role.grantedPermissions.push(_$permissionCheckbox.val());
      }
    }

    abp.ui.setBusy(_$modal);
    _roleService
      .create(role)
      .done(function () {
        _$modal.modal("hide");
        _$form[0].reset();
        abp.notify.info(l("SavedSuccessfully"));
        _$rolesTable.ajax.reload();
      })
      .always(function () {
        abp.ui.clearBusy(_$modal);
      });
  });

  $(document).on("click", ".delete-role", function () {
    var roleId = $(this).attr("data-role-id");
    var roleName = $(this).attr("data-role-name");

    deleteRole(roleId, roleName);
  });

  //$(document).on("click", ".edit-role", function (e) {
  //  var roleId = $(this).attr("data-role-id");

  //  e.preventDefault();
  //  abp.ajax({
  //    url: abp.appPath + "Roles/EditModal?roleId=" + roleId,
  //    type: "POST",
  //    dataType: "html",
  //    success: function (content) {
  //      $("#RoleEditModal div.modal-content").html(content);
  //    },
  //    error: function (e) {},
  //  });
  //});

  abp.event.on("role.edited", (data) => {
    _$rolesTable.ajax.reload();
  });

  function deleteRole(roleId, roleName) {
    abp.message.confirm(
      abp.utils.formatString(l("AreYouSureWantToDelete"), roleName),
      null,
      (isConfirmed) => {
        if (isConfirmed) {
          _roleService
            .delete({
              id: roleId,
            })
            .done(() => {
              abp.notify.info(l("SuccessfullyDeleted"));
              _$rolesTable.ajax.reload();
            });
        }
      },
    );
  }

  _$modal
    .on("shown.bs.modal", () => {
      _$modal.find("input:not([type=hidden]):first").focus();
    })
    .on("hidden.bs.modal", () => {
      _$form.clearForm();
    });

  $(".btn-search").on("click", (e) => {
    _$rolesTable.ajax.reload();
  });

  $(".txt-search").on("keypress", (e) => {
    if (e.which == 13) {
      _$rolesTable.ajax.reload();
      return false;
    }
  });
})(jQuery);
