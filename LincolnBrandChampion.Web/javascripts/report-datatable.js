﻿(function (window, document, $, undefined) {

    $.fn.dataTableExt.oApi.fnLengthChange = function (oSettings, iDisplay) {
        oSettings._iDisplayLength = iDisplay;
        oSettings.oApi._fnCalculateEnd(oSettings);

        /* If we have space to show extra rows (backing up from the end point - then do so */
        if (oSettings._iDisplayEnd == oSettings.aiDisplay.length) {
            oSettings._iDisplayStart = oSettings._iDisplayEnd - oSettings._iDisplayLength;
            if (oSettings._iDisplayStart < 0) {
                oSettings._iDisplayStart = 0;
            }
        }

        if (oSettings._iDisplayLength == -1) {
            oSettings._iDisplayStart = 0;
        }

        oSettings.oApi._fnDraw(oSettings);
    };


    $.fn.dataTable.LengthLinks = function (oSettings) {
        var container = $('<div></div>').addClass(oSettings.oClasses.sLength);
        var lastLength = -1;
        var draw = function () {
            // No point in updating - nothing has changed
            if (oSettings._iDisplayLength === lastLength) {
                return;
            }

            var menu = oSettings.aLengthMenu;
            var lang = menu.length === 2 && $.isArray(menu[0]) ? menu[1] : menu;
            var lens = menu.length === 2 && $.isArray(menu[0]) ? menu[0] : menu;

            var out = $.map(lens, function (el, i) {
                return el == oSettings._iDisplayLength ?
                  '<a class="active" data-length="' + lens[i] + '">' + lang[i] + '</a>' :
                  '<a data-length="' + lens[i] + '">' + lang[i] + '</a>';
            });

            container.html(oSettings.oLanguage.sLengthMenu.replace('_MENU_', out.join(' ')));
            lastLength = oSettings._iDisplayLength;
        };

        // API, so the feature wrapper can return the node to insert
        this.container = container;

        // Update on each draw
        oSettings.aoDrawCallback.push({
            "fn": function () {
                draw();
            },
            "sName": "PagingControl"
        });

        // Listen for events to change the page length
        container.on('click', 'a', function (e) {
            e.preventDefault();
            oSettings.oInstance.fnLengthChange(parseInt($(this).attr('data-length'), 10));
        });
    };

    // Subscribe the feature plug-in to DataTables, ready for use
    $.fn.dataTableExt.aoFeatures.push({
        "fnInit": function (oSettings) {
            var l = new $.fn.dataTable.LengthLinks(oSettings);
            return l.container[0];
        },
        "cFeature": "L",
        "sFeature": "LengthLinks"
    });


})(window, document, jQuery);
(function ($) {
    /*
     * Function: fnGetColumnData
     * Purpose:  Return an array of table values from a particular column.
     * Returns:  array string: 1d data array 
     * Inputs:   object:oSettings - dataTable settings object. This is always the last argument past to the function
     *           int:iColumn - the id of the column to extract the data from
     *           bool:bUnique - optional - if set to false duplicated values are not filtered out
     *           bool:bFiltered - optional - if set to false all the table data is used (not only the filtered)
     *           bool:bIgnoreEmpty - optional - if set to false empty values are not filtered from the result array
     * Author:   Benedikt Forchhammer <b.forchhammer /AT\ mind2.de>
     */
    $.fn.dataTableExt.oApi.fnGetColumnData = function (oSettings, iColumn, bUnique, bFiltered, bIgnoreEmpty) {
        // check that we have a column id
        if (typeof iColumn == "undefined") return new Array();

        // by default we only want unique data
        if (typeof bUnique == "undefined") bUnique = true;

        // by default we do want to only look at filtered data
        if (typeof bFiltered == "undefined") bFiltered = true;

        // by default we do not want to include empty values
        if (typeof bIgnoreEmpty == "undefined") bIgnoreEmpty = true;

        // list of rows which we're going to loop through
        var aiRows;

        // use only filtered rows
        if (bFiltered == true) aiRows = oSettings.aiDisplay;
            // use all rows
        else aiRows = oSettings.aiDisplayMaster; // all row numbers

        // set up data array    
        var asResultData = new Array();

        for (var i = 0, c = aiRows.length; i < c; i++) {
            iRow = aiRows[i];
            var aData = this.fnGetData(iRow);
            var sValue = aData[iColumn];

            // ignore empty values?
            if (bIgnoreEmpty == true && sValue.length == 0) continue;

                // ignore unique values?
            else if (bUnique == true && jQuery.inArray(sValue, asResultData) > -1) continue;

                // else push the value onto the result data array
            else asResultData.push(sValue);
        }

        return asResultData;
    }
}(jQuery));
/* Default class modification */
$.extend($.fn.dataTableExt.oStdClasses, {
    "sSortAsc": "header headerSortDown",
    "sSortDesc": "header headerSortUp",
    "sSortable": "header"
});

/* API method to get paging information */
$.fn.dataTableExt.oApi.fnPagingInfo = function (oSettings) {
    return {
        "iStart": oSettings._iDisplayStart,
        "iEnd": oSettings.fnDisplayEnd(),
        "iLength": oSettings._iDisplayLength,
        "iTotal": oSettings.fnRecordsTotal(),
        "iFilteredTotal": oSettings.fnRecordsDisplay(),
        "iPage": Math.ceil(oSettings._iDisplayStart / oSettings._iDisplayLength),
        "iTotalPages": Math.ceil(oSettings.fnRecordsDisplay() / oSettings._iDisplayLength)
    };
}

/* Bootstrap style pagination control */
$.extend($.fn.dataTableExt.oPagination, {
    "bootstrap": {
        "fnInit": function (oSettings, nPaging, fnDraw) {
            var oLang = oSettings.oLanguage.oPaginate;
            var fnClickHandler = function (e) {
                e.preventDefault();
                if (oSettings.oApi._fnPageChange(oSettings, e.data.action)) {
                    fnDraw(oSettings);
                }
            };

            $(nPaging).addClass('pagination').append(
                            '<ul>' +
                                '<li class="prev disabled"><a href="#"> ' + oLang.sPrevious + '</a></li>' +
                                '<li class="next disabled"><a href="#">' + oLang.sNext + ' </a></li>' +
                            '</ul>'
                        );
            var els = $('a', nPaging);
            $(els[0]).bind('click.DT', { action: "previous" }, fnClickHandler);
            $(els[1]).bind('click.DT', { action: "next" }, fnClickHandler);
        },

        "fnUpdate": function (oSettings, fnDraw) {
            var iListLength = 5;
            var oPaging = oSettings.oInstance.fnPagingInfo();
            var an = oSettings.aanFeatures.p;
            var i, j, sClass, iStart, iEnd, iHalf = Math.floor(iListLength / 2);


            if (oPaging.iTotalPages < iListLength) {
                iStart = 1;
                iEnd = oPaging.iTotalPages;
            }
            else if (oPaging.iPage <= iHalf) {
                iStart = 1;
                iEnd = iListLength;
            } else if (oPaging.iPage >= (oPaging.iTotalPages - iHalf)) {
                iStart = oPaging.iTotalPages - iListLength + 1;
                iEnd = oPaging.iTotalPages;
            } else {
                iStart = oPaging.iPage - iHalf + 1;
                iEnd = iStart + iListLength - 1;
            }

            for (i = 0, iLen = an.length; i < iLen; i++) {
                // Remove the middle elements
                $('li:gt(0)', an[i]).filter(':not(:last)').remove();

                // Add the new list items and their event handlers
                for (j = iStart; j <= iEnd; j++) {
                    sClass = (j == oPaging.iPage + 1) ? 'class="active"' : '';
                    $('<li ' + sClass + '><a href="#">' + j + '</a></li>')
                                    .insertBefore($('li:last', an[i])[0])
                                    .bind('click', function (e) {
                                        e.preventDefault();
                                        oSettings._iDisplayStart = (parseInt($('a', this).text(), 10) - 1) * oPaging.iLength;
                                        fnDraw(oSettings);
                                    });
                }

                // Add / remove disabled classes from the static elements
                if (oPaging.iPage === 0) {
                    $('li:first', an[i]).addClass('disabled');
                } else {
                    $('li:first', an[i]).removeClass('disabled');
                }

                if (oPaging.iPage === oPaging.iTotalPages - 1 || oPaging.iTotalPages === 0) {
                    $('li:last', an[i]).addClass('disabled');
                } else {
                    $('li:last', an[i]).removeClass('disabled');
                }
            }
        }
    }
});
function fnCreateSelect(aData) {
    var r = '<div class="custom dropdown select"><select><option value=""></option>', i, iLen = aData.length;
    for (i = 0 ; i < iLen ; i++) {
        r += '<option value="' + aData[i] + '">' + aData[i] + '</option>';
    }
    return r + '</select><span class="selector"></span></div>';
}

$(document).ready(function () {
    var oTable = $('#regTable').dataTable(
        {
            "sDom": "<'row'<'span8'><'span8'>r>t<'row'<'span8'i><'span8'p>>",
            "sPaginationType": "bootstrap",
            "bDeferRender": true,
            "bSortCellsTop": true,
            "bRetrieve": true,
            "bJQueryUI": true,
            "bDestroy": true,
            "oLanguage": {
                "sLengthMenu": "_MENU_ records per page",
                "oPaginate": {
                    "sPrevious": "&lt;prev",
                    "sNext": "next&gt;",
                }
            },
            "columnDefs": [{
                "targets": 10,
                "searchable": true,
                "defaultContent": "&nbsp",
            }],
        });
    var filteredColumnIndexMap = {};

    function makeFilterable() {
        $("#filter-row th").each(function (i) {
            if ($(this).hasClass("filtered")) {
                $(this).html(fnCreateSelect(oTable.fnGetColumnData(i)));
                $('select', this).change(function (e) {
                    oTable.fnFilter($(this).val(), i);
                    if ($(this).val() == "") {
                        delete filteredColumnIndexMap[i];
                    }
                    else {
                        filteredColumnIndexMap[i] = $(this).val();
                    }
                    oTable.fnFilter($(this).val(), i);
                    filterSelectData(i);
                });
            }
        });
    }

    /* Add a select menu for each TH element in the table footer */
    $.when(makeFilterable()).then(function () {
        $("#loading").hide();
        $("#table-wrap").show();
    });
    function filterSelectData(currentColumIndex) {
        $("#filter-row th").each(function (i) {
            if ($(this).hasClass("filtered")) {
                if (filteredColumnIndexMap[i] == null) {
                    $(this).html(fnCreateSelect(oTable.fnGetColumnData(i)));
                    $('select', this).change(function (e) {
                        if ($(this).val() == "") {
                            delete filteredColumnIndexMap[i];
                        }
                        else {
                            filteredColumnIndexMap[i] = $(this).val();
                        }
                        oTable.fnFilter($(this).val(), i);
                        filterSelectData(i);
                    });
                }
            }
        });
    }

    $("#excel-btn, #pdf-btn").click(function () {
        var starsIdList = oTable.fnGetColumnData(0, false);
        var eventIdList = oTable.fnGetColumnData(1, false);
        var form = $("<form id='excel-form' action='" + $(this).attr("href") + "' method='POST'></form>");
        for (i = 0 ; i < starsIdList.length ; i++) {
            form.append("<input type='hidden' name='StarsIdList[" + i + "]' value='" + starsIdList[i] + "' />");
            form.append("<input type='hidden' name='EventIdList[" + i + "]' value='" + eventIdList[i] + "' />");
        }
        form.submit();
        return false;
    });

    $(document).on("change", "select[name='regTable_length']", function () {
        var table = $('#regTable').DataTable();
        table.page.len($(this).val()).draw();
    });

    $(document).on("keyup", "input[name='regTable_search']", function () {
        var table = $('#regTable').DataTable();
        table.search(this.value).draw();
    });

    var paging = $("#regTable_paginate");
    paging.appendTo(".report-controls div:first-child");
});