(function ($) {
	app.modals.CreateLanguagesModal = function () {
		var _modalManager;
		var _$form = null;
		var _$table = $("#LanguagesTable");
		var _languagesAppService = abp.services.app.languages;

		const tagifyLanguageName = new Tagify($("#LanguageName")[0], {
			enforceWhitelist: true,
			whitelist: [
				{ value: "vi", label: "Việt Nam" },
				{ value: "en", label: "English" }
			],
			tagTextProp: "label",
			maxTags: 1,
			dropdown: {
				maxItems: 10,
				enabled: 0,
				closeOnSelect: false,
				mapValueTo: "label"
			}
		});
		const tagifySource = new Tagify($("#Source")[0], {
			enforceWhitelist: true,
			whitelist: [
				"SaaS"
			],
			maxTags: 1,
			dropdown: {
				maxItems: 10,
				enabled: 0,
				closeOnSelect: false,
			}
		});

		this.init = function (modalManager) {
			_modalManager = modalManager;
			_$form = _modalManager.getModal().find('#CreateLanguagesForm');

			$("#LanguageName").on("change", function () {
				_$form.validate().element(this);
			});
			$("#Source").on("change", function () {
				_$form.validate().element(this);
			});

			_$form.validate({
				rules: {
					Key: {
						required: true,
						minlength: 2,
					},
					Value: {
						required: true,
						minlength: 2,
					},
					LanguageName: {
						required: true,
						minlength: 2,
					},
					Source: {
						required: true,
						minlength: 2,
					}
				},
				messages: {
					Key: {
						required: abp.localization.localize("PleaseEnterAKey", "VietNamTourism"),
						minlength: abp.localization.localize("KeyMustConsistOfAtLeast2Characters", "VietNamTourism")
					},
					Value: {
						required: abp.localization.localize("PleaseEnterAValue", "VietNamTourism"),
						minlength: abp.localization.localize("ValueMustConsistOfAtLeast2Characters", "VietNamTourism")
					},
					LanguageName: {
						required: abp.localization.localize("PleaseEnterALanguageName", "VietNamTourism"),
						minlength: abp.localization.localize("LanguageNameMustConsistOfAtLeast2Characters", "VietNamTourism")
					},
					Source: {
						required: abp.localization.localize("PleaseEnterASource", "VietNamTourism"),
						minlength: abp.localization.localize("SourceMustConsistOfAtLeast2Characters", "VietNamTourism")
					}
				},
				errorClass: "text-danger",
				errorPlacement: function (error, element) {
					error.insertAfter(element);
				}
			})


		}
		this.save = function () {
			if (!_$form.valid()) return;

			var dataInput = {};
			dataInput.Key = $("#Key").val();
			dataInput.Value = $("#Value").val();
			dataInput.LanguageName = tagifyLanguageName.value[0].value;
			dataInput.Source = tagifySource.value[0].value;

			abp.ui.setBusy(_modalManager.getModal());
			_languagesAppService
				.create(dataInput)
				.done(function (result) {
					abp.notify.info(abp.localization.localize("SavedSuccessfully"), "VietNamTourism");
					_modalManager.close();
					_$table.DataTable().ajax.reload();
				})
				.always(function () {
					abp.ui.clearBusy(_modalManager.getModal());
				});
		}
	}
})(jQuery);