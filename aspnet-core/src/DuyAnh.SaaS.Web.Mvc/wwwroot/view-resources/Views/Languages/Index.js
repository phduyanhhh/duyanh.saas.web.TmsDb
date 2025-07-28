(function ($) {
	var _languageAppService = abp.services.app.languages;
  _$table = $("#LanguagesTable");

  var _createUploadModal = new app.ModalManager({
    viewUrl: abp.appPath + 'Languages/UploadFile',
    scriptUrl: abp.appPath + 'view-resources/Views/Languages/_UploadFile.js',
    modalClass: 'UploadFileLanguagesModal',
    modalType: 'modal-xl'
  });
  var _createLanguageModal = new app.ModalManager({
    viewUrl: abp.appPath + 'Languages/Create',
    scriptUrl: abp.appPath + 'view-resources/Views/Languages/_Create.js',
    modalClass: 'CreateLanguagesModal',
    modalType: 'modal-xl'
  });

  $("#UploadInputLanguages").on('click', function () {
    _createUploadModal.open();
  });
  $("#CreateLanguages").on('click', function () {
    _createLanguageModal.open();
  });

  var _$languagesTable = _$table.DataTable({
    paging: true,
    serverSide: true,
    processing: true,
    listAction: {
      ajaxFunction: _languageAppService.getAllLanguages,
      inputFilter: function () {
        return $("#LanguagesSearchForm").serializeFormToObject(true);
      },
    },
    buttons: [
      {
        name: "refresh",
        text: '<i class="fas fa-redo-alt"></i>',
        action: () => _$languagesTable.draw(false),
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
        className: "dt-center",
        data: "key",
        render: (data, type, row, meta) => {
          return row.key;
        }
      },
      {
        targets: 2,
        className: "dt-center",
        data: "value",
        render: (data, type, row, meta) => {
          return row.value;
        }
      },
      {
        targets: 3,
        className: "dt-center",
        data: "languageName",
        render: (data, type, row, meta) => {
          return row.languageName;
        }
      },
      {
        targets: 4,
        className: "dt-center",
        data: "source",
        render: (data, type, row, meta) => {
          return row.source;
        }
      },
      {
        targets: 5,
        data: null,
        className: 'dt-center',
        orderable: false,
        autoWidth: false,
        defaultContent: '',
        render: (data, type, row, meta) => {
          return `
            <div class="dropdown">
              <button class="btn btn-primary btn-sm dropdown-toggle" type="button" id="dropdownMenuButton-${row.id}" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                Actions
              </button>
              <div class="dropdown-menu" aria-labelledby="dropdownMenuButton-${row.id}">
                <a class="dropdown-item btn-edit" id="btn-edit" href="#" data-id="${row.id}">Edit</a>
                <a class="dropdown-item btn-delete" href="#" data-id="${row.id}" data-name="${row.displayName}">Delete</a>
              </div>
            </div>
          `;
        }
      }
    ],
  });
})(jQuery);