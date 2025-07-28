(function ($) {
	app.modals.RolesCreateModal = function () {
		var _roleService = abp.services.app.role;
		var _modalManager;
		var _$form = null
		var _$table = $("#RolesTable");
		this.init = function (modalManager) {
			_modalManager = modalManager;
			_$form = _modalManager.getModal().find('#roleCreateForm');

			_$form.validate({
				rules: {
					Name: {
						required: true,
						minlength: 2,
					},
					DisplayName: {
						required: true,
						minlength: 2,
					}
				},
				messages: {
					Name: {
						required: abp.localization.localize("PleaseEnterAName", "SaaS"),
						minlength: abp.localization.localize("NameMustConsistOfAtLeast2Characters", "SaaS")
					},
					DisplayName: {
						required: abp.localization.localize("PleaseEnterADisplayName", "SaaS"),
						minlength: abp.localization.localize("DisplayNameMustConsistOfAtLeast2Characters", "SaaS")
					}
				},
				errorClass: "text-danger",
				errorPlacement: function (error, element) {
					error.insertAfter(element);
				}
			})
		}
		this.save = function () {
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

			abp.ui.setBusy(_modalManager.getModal());
			_roleService
				.create(role)
				.done(function () {
					_modalManager.close();
					_$form[0].reset();
					abp.notify.info(abp.localization.localize("SavedSuccessfully", "SaaS"));
					_$table.DataTable().ajax.reload();
				})
				.always(function () {
					abp.ui.clearBusy(_modalManager.getModal());
				});
		}
	}
})(jQuery);