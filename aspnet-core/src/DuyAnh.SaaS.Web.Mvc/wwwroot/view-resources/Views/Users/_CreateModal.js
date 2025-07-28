(function ($) {
	app.modals.UsersCreateModal = function () {
		var _userService = abp.services.app.user;
		var _modalManager;
		var _$form = null
		var _$table = $("#UsersTable");
		this.init = function (modalManager) {
			_modalManager = modalManager;

			_$form = _modalManager.getModal().find('#userCreateForm');

			_$form.validate({
				rules: {
					UserName: {
						required: true,
						minlength: 2,
					},
					Name: {
						required: true,
						minlength: 2,
					},
					Surname: {
						required: true,
						minlength: 2,
					},
					EmailAddress: {
						required: true,
						email: true
					},
					Password: {
						required: true,
						minlength: 6,
					},
					ConfirmPassword: {
						required: true,
						equalTo: "#Password"
					}
				},
				messages: {
					UserName: {
						required: abp.localization.localize("PleaseEnterAUserName", "SaaS"),
						minlength: abp.localization.localize("UserNameMustConsistOfAtLeast2Characters", "SaaS")
					},
					Name: {
						required: abp.localization.localize("PleaseEnterAName", "SaaS"),
						minlength: abp.localization.localize("NameMustConsistOfAtLeast2Characters", "SaaS")
					},
					Surname: {
						required: abp.localization.localize("PleaseEnterASurname", "SaaS"),
						minlength: abp.localization.localize("SurnameMustConsistOfAtLeast2Characters", "SaaS")
					},
					EmailAddress: {
						required: abp.localization.localize("PleaseEnterAEmail", "SaaS"),
						email: abp.localization.localize("InvalidEmailFormat", "SaaS")
					},
					Password: {
						required: abp.localization.localize("PleaseEnterAPassword", "SaaS"),
						minlength: abp.localization.localize("PasswordMustConsistOfAtLeast6Characters", "SaaS")
					},
					ConfirmPassword: {
						required: abp.localization.localize("PleaseEnterARePassword", "SaaS"),
						equalTo: abp.localization.localize("RePasswordsDoNotMatch", "SaaS")
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
			var user = _$form.serializeFormToObject();
			user.roleNames = [];
			var _$roleCheckboxes = _$form[0].querySelectorAll(
				"input[name='role']:checked",
			);
			if (_$roleCheckboxes) {
				for (
					var roleIndex = 0;
					roleIndex < _$roleCheckboxes.length;
					roleIndex++
				) {
					var _$roleCheckbox = $(_$roleCheckboxes[roleIndex]);
					user.roleNames.push(_$roleCheckbox.val());
				}
			}

			abp.ui.setBusy(_modalManager.getModal());
			_userService
				.create(user)
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