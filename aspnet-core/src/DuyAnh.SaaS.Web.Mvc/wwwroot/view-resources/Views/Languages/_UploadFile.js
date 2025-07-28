(function ($) {
	app.modals.UploadFileLanguagesModal = function () {
		var _modalManager;
		var _$form = null;
		var _$table = $("#LanguagesTable");
		var _languagesAppService = abp.services.app.languages;

		this.init = function(modalManager) {
			_modalManager = modalManager;

			_$form = _modalManager.getModal().find('#UploadLanguagesForm');

			$(document).ready(function () {
				bsCustomFileInput.init()
			});

			$.validator.addMethod("fileExtension", function (value, element, param) {
				if (value) {
					var ext = value.split('.').pop().toLowerCase();
					return param.split(',').includes(ext);
				}
				return true; // Nếu không có giá trị (và không bắt buộc) thì hợp lệ
			}, abp.localization.localize("PleaseAddFileWithJsonExtension", "VietNamTourism"));

			$('#customFile').on('change', function () {
				$('#UploadLanguagesForm').validate().element(this);
			});

			_$form.validate({
				rules: {
					fileInput: {
						required: true,
						fileExtension: "json"
					}
				},
				messages: {
					required: abp.localization.localize("PleaseAddFileToInputField", "VietNamTourism"),
					fileExtension: abp.localization.localize("PleaseAddFileWithJsonExtension", "VietNamTourism")
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
			var file = _$form.find('#customFile')[0].files[0];
			var formData = new FormData();
			formData.append("fileJson", file);

			abp.ui.setBusy(_modalManager.getModal());

			$.ajax({
				url: abp.appPath + "api/services/app/Languages/UploadFileJsonLanguage", // Đây là endpoint thực sự của controller
				type: "POST",
				data: formData,
				contentType: false,
				processData: false,
				success: function () {
					abp.notify.info("Tải lên thành công!");
					_modalManager.close();
					_$table.DataTable().ajax.reload();
				},
				error: function (xhr) {
					abp.notify.error("Lỗi khi tải lên: " + (xhr.responseText || "Không xác định"));
				},
				complete: function () {
					abp.ui.clearBusy(_modalManager.getModal());
				}
			});
		}
	}
})(jQuery);