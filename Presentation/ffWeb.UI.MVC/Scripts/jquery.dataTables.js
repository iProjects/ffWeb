/*! DataTables 1.10.2
 * ©2008-2014 SpryMedia Ltd - datatables.net/license
 */

/**
 * @summary     DataTables
 * @description Paginate, search and order HTML tables
 * @version     1.10.2
 * @file        jquery.dataTables.js
 * @author      SpryMedia Ltd (www.sprymedia.co.uk)
 * @contact     www.sprymedia.co.uk/contact
 * @copyright   Copyright 2008-2014 SpryMedia Ltd.
 *
 * This source file is free software, available under the following license:
 *   MIT license - http://datatables.net/license
 *
 * This source file is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE. See the license files for details.
 *
 * For details please refer to: http://www.datatables.net
 */

/*jslint evil: true, undef: true, browser: true */
/*globals $,require,jQuery,define,_selector_run,_selector_opts,_selector_first,_selector_row_indexes,_ext,_Api,_api_register,_api_registerPlural,_re_new_lines,_re_html,_re_formatted_numeric,_re_escape_regex,_empty,_intVal,_numToDecimal,_isNumber,_isHtml,_htmlNumeric,_pluck,_pluck_order,_range,_stripHtml,_unique,_fnBuildAjax,_fnAjaxUpdate,_fnAjaxParameters,_fnAjaxUpdateDraw,_fnAjaxDataSrc,_fnAddColumn,_fnColumnOptions,_fnAdjustColumnSizing,_fnVisibleToColumnIndex,_fnColumnIndexToVisible,_fnVisbleColumns,_fnGetColumns,_fnColumnTypes,_fnApplyColumnDefs,_fnHungarianMap,_fnCamelToHungarian,_fnLanguageCompat,_fnBrowserDetect,_fnAddData,_fnAddTr,_fnNodeToDataIndex,_fnNodeToColumnIndex,_fnGetCellData,_fnSetCellData,_fnSplitObjNotation,_fnGetObjectDataFn,_fnSetObjectDataFn,_fnGetDataMaster,_fnClearTable,_fnDeleteIndex,_fnInvalidateRow,_fnGetRowElements,_fnCreateTr,_fnBuildHead,_fnDrawHead,_fnDraw,_fnReDraw,_fnAddOptionsHtml,_fnDetectHeader,_fnGetUniqueThs,_fnFeatureHtmlFilter,_fnFilterComplete,_fnFilterCustom,_fnFilterColumn,_fnFilter,_fnFilterCreateSearch,_fnEscapeRegex,_fnFilterData,_fnFeatureHtmlInfo,_fnUpdateInfo,_fnInfoMacros,_fnInitialise,_fnInitComplete,_fnLengthChange,_fnFeatureHtmlLength,_fnFeatureHtmlPaginate,_fnPageChange,_fnFeatureHtmlProcessing,_fnProcessingDisplay,_fnFeatureHtmlTable,_fnScrollDraw,_fnApplyToChildren,_fnCalculateColumnWidths,_fnThrottle,_fnConvertToWidth,_fnScrollingWidthAdjust,_fnGetWidestNode,_fnGetMaxLenString,_fnStringToCss,_fnScrollBarWidth,_fnSortFlatten,_fnSort,_fnSortAria,_fnSortListener,_fnSortAttachListener,_fnSortingClasses,_fnSortData,_fnSaveState,_fnLoadState,_fnSettingsFromNode,_fnLog,_fnMap,_fnBindAction,_fnCallbackReg,_fnCallbackFire,_fnLengthOverflow,_fnRenderer,_fnDataSource,_fnRowAttributes*/

(/** @lends <global> */function( window, document, undefined ) {

    (function (factory) {
        "use strict";

        if (typeof define === 'function' && define.amd) {
            // Define as an AMD module if possible
            define('datatables', ['jquery'], factory);
        }
        else if (typeof exports === 'object') {
            // Node/CommonJS
            factory(require('jquery'));
        }
        else if (jQuery && !jQuery.fn.dataTable) {
            // Define using browser globals otherwise
            // Prevent multiple instantiations if the script is loaded twice
            factory(jQuery);
        }
    }
    (/** @lends <global> */function ($) {
        "use strict";

        /**
         * DataTables is a plug-in for the jQuery Javascript library. It is a highly
         * flexible tool, based upon the foundations of progressive enhancement,
         * which will add advanced interaction controls to any HTML table. For a
         * full list of features please refer to
         * [DataTables.net](href="http://datatables.net).
         *
         * Note that the `DataTable` object is not a global variable but is aliased
         * to `jQuery.fn.DataTable` and `jQuery.fn.dataTable` through which it may
         * be  accessed.
         *
         *  @class
         *  @param {object} [init={}] Configuration object for DataTables. Options
         *    are defined by {@link DataTable.defaults}
         *  @requires jQuery 1.7+
         *
         *  @example
         *    // Basic initialisation
         *    $(document).ready( function {
         *      $('#example').dataTable();
         *    } );
         *
         *  @example
         *    // Initialisation with configuration options - in this case, disable
         *    // pagination and sorting.
         *    $(document).ready( function {
         *      $('#example').dataTable( {
         *        "paginate": false,
         *        "sort": false
         *      } );
         *    } );
         */
        var DataTable;


        /*
         * It is useful to have variables which are scoped locally so only the
         * DataTables functions can access them and they don't leak into global space.
         * At the same time these functions are often useful over multiple files in the
         * core and API, so we list, or at least document, all variables which are used
         * by DataTables as private variables here. This also ensures that there is no
         * clashing of variable names and that they can easily referenced for reuse.
         */


        // Defined else where
        //  _selector_run
        //  _selector_opts
        //  _selector_first
        //  _selector_row_indexes

        var _ext; // DataTable.ext
        var _Api; // DataTable.Api
        var _api_register; // DataTable.Api.register
        var _api_registerPlural; // DataTable.Api.registerPlural

        var _re_dic = {};
        var _re_new_lines = /[\r\n]/g;
        var _re_html = /<.*?>/g;
        var _re_date_start = /^[\w\+\-]/;
        var _re_date_end = /[\w\+\-]$/;

        // Escape regular expression special characters
        var _re_escape_regex = new RegExp('(\\' + ['/', '.', '*', '+', '?', '|', '(', ')', '[', ']', '{', '}', '\\', '$', '^', '-'].join('|\\') + ')', 'g');

        // U+2009 is thin space and U+202F is narrow no-break space, both used in many
        // standards as thousands separators
        var _re_formatted_numeric = /[',$£€¥%\u2009\u202F]/g;


        var _empty = function (d) {
            return !d || d === true || d === '-' ? true : false;
        };


        var _intVal = function (s) {
            var integer = parseInt(s, 10);
            return !isNaN(integer) && isFinite(s) ? integer : null;
        };

        // Convert from a formatted number with characters other than `.` as the
        // decimal place, to a Javascript number
        var _numToDecimal = function (num, decimalPoint) {
            // Cache created regular expressions for speed as this function is called often
            if (!_re_dic[decimalPoint]) {
                _re_dic[decimalPoint] = new RegExp(_fnEscapeRegex(decimalPoint), 'g');
            }
            return typeof num === 'string' ?
                num.replace(/\./g, '').replace(_re_dic[decimalPoint], '.') :
                num;
        };


        var _isNumber = function (d, decimalPoint, formatted) {
            var strType = typeof d === 'string';

            if (decimalPoint && strType) {
                d = _numToDecimal(d, decimalPoint);
            }

            if (formatted && strType) {
                d = d.replace(_re_formatted_numeric, '');
            }

            return _empty(d) || (!isNaN(parseFloat(d)) && isFinite(d));
        };


        // A string without HTML in it can be considered to be HTML still
        var _isHtml = function (d) {
            return _empty(d) || typeof d === 'string';
        };


        var _htmlNumeric = function (d, decimalPoint, formatted) {
            if (_empty(d)) {
                return true;
            }

            var html = _isHtml(d);
            return !html ?
                null :
                _isNumber(_stripHtml(d), decimalPoint, formatted) ?
                    true :
                    null;
        };


        var _pluck = function (a, prop, prop2) {
            var out = [];
            var i = 0, ien = a.length;

            // Could have the test in the loop for slightly smaller code, but speed
            // is essential here
            if (prop2 !== undefined) {
                for (; i < ien ; i++) {
                    if (a[i] && a[i][prop]) {
                        out.push(a[i][prop][prop2]);
                    }
                }
            }
            else {
                for (; i < ien ; i++) {
                    if (a[i]) {
                        out.push(a[i][prop]);
                    }
                }
            }

            return out;
        };


        // Basically the same as _pluck, but rather than looping over `a` we use `order`
        // as the indexes to pick from `a`
        var _pluck_order = function (a, order, prop, prop2) {
            var out = [];
            var i = 0, ien = order.length;

            // Could have the test in the loop for slightly smaller code, but speed
            // is essential here
            if (prop2 !== undefined) {
                for (; i < ien ; i++) {
                    out.push(a[order[i]][prop][prop2]);
                }
            }
            else {
                for (; i < ien ; i++) {
                    out.push(a[order[i]][prop]);
                }
            }

            return out;
        };


        var _range = function (len, start) {
            var out = [];
            var end;

            if (start === undefined) {
                start = 0;
                end = len;
            }
            else {
                end = start;
                start = len;
            }

            for (var i = start ; i < end ; i++) {
                out.push(i);
            }

            return out;
        };


        var _stripHtml = function (d) {
            return d.replace(_re_html, '');
        };


        /**
         * Find the unique elements in a source array.
         *
         * @param  {array} src Source array
         * @return {array} Array of unique items
         * @ignore
         */
        var _unique = function (src) {
            // A faster unique method is to use object keys to identify used values,
            // but this doesn't work with arrays or objects, which we must also
            // consider. See jsperf.com/compare-array-unique-versions/4 for more
            // information.
            var
                out = [],
                val,
                i, ien = src.length,
                j, k = 0;

            again: for (i = 0 ; i < ien ; i++) {
                val = src[i];

                for (j = 0 ; j < k ; j++) {
                    if (out[j] === val) {
                        continue again;
                    }
                }

                out.push(val);
                k++;
            }

            return out;
        };



        /**
         * Create a mapping object that allows camel case parameters to be looked up
         * for their Hungarian counterparts. The mapping is stored in a private
         * parameter called `_hungarianMap` which can be accessed on the source object.
         *  @param {object} o
         *  @memberof DataTable#oApi
         */
        function _fnHungarianMap(o) {
            var
                hungarian = 'a aa ai ao as b fn i m o s ',
                match,
                newKey,
                map = {};

            $.each(o, function (key, val) {
                match = key.match(/^([^A-Z]+?)([A-Z])/);

                if (match && hungarian.indexOf(match[1] + ' ') !== -1) {
                    newKey = key.replace(match[0], match[2].toLowerCase());
                    map[newKey] = key;

                    //console.log( key, match );
                    if (match[1] === 'o') {
                        _fnHungarianMap(o[key]);
                    }
                }
            });

            o._hungarianMap = map;
        }


        /**
         * Convert from camel case parameters to Hungarian, based on a Hungarian map
         * created by _fnHungarianMap.
         *  @param {object} src The model object which holds all parameters that can be
         *    mapped.
         *  @param {object} user The object to convert from camel case to Hungarian.
         *  @param {boolean} force When set to `true`, properties which already have a
         *    Hungarian value in the `user` object will be overwritten. Otherwise they
         *    won't be.
         *  @memberof DataTable#oApi
         */
        function _fnCamelToHungarian(src, user, force) {
            if (!src._hungarianMap) {
                _fnHungarianMap(src);
            }

            var hungarianKey;

            $.each(user, function (key, val) {
                hungarianKey = src._hungarianMap[key];

                if (hungarianKey !== undefined && (force || user[hungarianKey] === undefined)) {
                    // For objects, we need to buzz down into the object to copy parameters
                    if (hungarianKey.charAt(0) === 'o') {
                        // Copy the camelCase options over to the hungarian
                        if (!user[hungarianKey]) {
                            user[hungarianKey] = {};
                        }
                        $.extend(true, user[hungarianKey], user[key]);

                        _fnCamelToHungarian(src[hungarianKey], user[hungarianKey], force);
                    }
                    else {
                        user[hungarianKey] = user[key];
                    }
                }
            });
        }


        /**
         * Language compatibility - when certain options are given, and others aren't, we
         * need to duplicate the values over, in order to provide backwards compatibility
         * with older language files.
         *  @param {object} oSettings dataTables settings object
         *  @memberof DataTable#oApi
         */
        function _fnLanguageCompat(lang) {
            var defaults = DataTable.defaults.oLanguage;
            var zeroRecords = lang.sZeroRecords;

            /* Backwards compatibility - if there is no sEmptyTable given, then use the same as
             * sZeroRecords - assuming that is given.
             */
            if (!lang.sEmptyTable && zeroRecords &&
                defaults.sEmptyTable === "No data available in table") {
                _fnMap(lang, lang, 'sZeroRecords', 'sEmptyTable');
            }

            /* Likewise with loading records */
            if (!lang.sLoadingRecords && zeroRecords &&
                defaults.sLoadingRecords === "Loading...") {
                _fnMap(lang, lang, 'sZeroRecords', 'sLoadingRecords');
            }

            // Old parameter name of the thousands separator mapped onto the new
            if (lang.sInfoThousands) {
                lang.sThousands = lang.sInfoThousands;
            }

            var decimal = lang.sDecimal;
            if (decimal) {
                _addNumericSort(decimal);
            }
        }


        /**
         * Map one parameter onto another
         *  @param {object} o Object to map
         *  @param {*} knew The new parameter name
         *  @param {*} old The old parameter name
         */
        var _fnCompatMap = function (o, knew, old) {
            if (o[knew] !== undefined) {
                o[old] = o[knew];
            }
        };


        /**
         * Provide backwards compatibility for the main DT options. Note that the new
         * options are mapped onto the old parameters, so this is an external interface
         * change only.
         *  @param {object} init Object to map
         */
        function _fnCompatOpts(init) {
            _fnCompatMap(init, 'ordering', 'bSort');
            _fnCompatMap(init, 'orderMulti', 'bSortMulti');
            _fnCompatMap(init, 'orderClasses', 'bSortClasses');
            _fnCompatMap(init, 'orderCellsTop', 'bSortCellsTop');
            _fnCompatMap(init, 'order', 'aaSorting');
            _fnCompatMap(init, 'orderFixed', 'aaSortingFixed');
            _fnCompatMap(init, 'paging', 'bPaginate');
            _fnCompatMap(init, 'pagingType', 'sPaginationType');
            _fnCompatMap(init, 'pageLength', 'iDisplayLength');
            _fnCompatMap(init, 'searching', 'bFilter');

            // Column search objects are in an array, so it needs to be converted
            // element by element
            var searchCols = init.aoSearchCols;

            if (searchCols) {
                for (var i = 0, ien = searchCols.length ; i < ien ; i++) {
                    if (searchCols[i]) {
                        _fnCamelToHungarian(DataTable.models.oSearch, searchCols[i]);
                    }
                }
            }
        }


        /**
         * Provide backwards compatibility for column options. Note that the new options
         * are mapped onto the old parameters, so this is an external interface change
         * only.
         *  @param {object} init Object to map
         */
        function _fnCompatCols(init) {
            _fnCompatMap(init, 'orderable', 'bSortable');
            _fnCompatMap(init, 'orderData', 'aDataSort');
            _fnCompatMap(init, 'orderSequence', 'asSorting');
            _fnCompatMap(init, 'orderDataType', 'sortDataType');
        }


        /**
         * Browser feature detection for capabilities, quirks
         *  @param {object} settings dataTables settings object
         *  @memberof DataTable#oApi
         */
        function _fnBrowserDetect(settings) {
            var browser = settings.oBrowser;

            // Scrolling feature / quirks detection
            var n = $('<div/>')
                .css({
                    position: 'absolute',
                    top: 0,
                    left: 0,
                    height: 1,
                    width: 1,
                    overflow: 'hidden'
                })
                .append(
                    $('<div/>')
                        .css({
                            position: 'absolute',
                            top: 1,
                            left: 1,
                            width: 100,
                            overflow: 'scroll'
                        })
                        .append(
                            $('<div class="test"/>')
                                .css({
                                    width: '100%',
                                    height: 10
                                })
                        )
                )
                .appendTo('body');

            var test = n.find('.test');

            // IE6/7 will oversize a width 100% element inside a scrolling element, to
            // include the width of the scrollbar, while other browsers ensure the inner
            // element is contained without forcing scrolling
            browser.bScrollOversize = test[0].offsetWidth === 100;

            // In rtl text layout, some browsers (most, but not all) will place the
            // scrollbar on the left, rather than the right.
            browser.bScrollbarLeft = test.offset().left !== 1;

            n.remove();
        }


        /**
         * Array.prototype reduce[Right] method, used for browsers which don't support
         * JS 1.6. Done this way to reduce code size, since we iterate either way
         *  @param {object} settings dataTables settings object
         *  @memberof DataTable#oApi
         */
        function _fnReduce(that, fn, init, start, end, inc) {
            var
                i = start,
                value,
                isSet = false;

            if (init !== undefined) {
                value = init;
                isSet = true;
            }

            while (i !== end) {
                if (!that.hasOwnProperty(i)) {
                    continue;
                }

                value = isSet ?
                    fn(value, that[i], i, that) :
                    that[i];

                isSet = true;
                i += inc;
            }

            return value;
        }

        /**
         * Add a column to the list used for the table with default values
         *  @param {object} oSettings dataTables settings object
         *  @param {node} nTh The th element for this column
         *  @memberof DataTable#oApi
         */
        function _fnAddColumn(oSettings, nTh) {
            // Add column to aoColumns array
            var oDefaults = DataTable.defaults.column;
            var iCol = oSet
        }





    }));

});