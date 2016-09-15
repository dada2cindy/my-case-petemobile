/*
Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function( config )
{
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';
	config.width = 950;
	config.height = 350;
	config.pasteFromWordRemoveFontStyles = false;
	config.pasteFromWordRemoveStyles = false;
	//拼字檢查預設不啟動
	config.scayt_autoStartup = false;
    //移除自動拼字檢查
	config.removePlugins = 'scayt';
	config.font_names = '新細明體/新細明體;細明體/細明體;標楷體/標楷體;微軟正黑體/微軟正黑體;' + config.font_names;
};
