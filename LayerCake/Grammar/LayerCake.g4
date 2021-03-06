grammar LayerCake;

// GRAMMAR

config: global_statement+ EOF;

global_statement:
	empty
	| swap_statement EOL
	| swap_statement EOL
	| is_statement EOL
	| toggle_statement EOL
	| when_block
	| layer_block;

empty: (WS* EOL)+;

is_statement:
	KEY_NAME IS_KEYWORD KEY_NAME
	| KEY_NAME IS_KEYWORD NOTHING_KEYWORD;

extends_statement:
	EXTENDS_KEYWORD ID_NAME
	| EXTENDS_KEYWORD NOTHING_KEYWORD;

toggle_statement: TOGGLE_KEYWORD ID_NAME WITH_KEYWORD KEY_NAME;

set_statement: is_statement;

layer_statement:
	empty
	| swap_statement EOL
	| swap_statement when_statement EOL
	| toggle_statement EOL
	| when_block EOL
	| is_statement EOL;

layer_statements: layer_statement+;

when_statement: WHEN_KEYWORD 'bundle is' STRING EOL;

when_block: 
	when_statement layer_statements DONE_KEYWORD;

swap_statement:
	SWAP_KEYWORD KEY_NAME AND_KEYWORD KEY_NAME
	| SWAP_KEYWORD KEY_NAME AND_KEYWORD KEY_NAME when_statement;

layer_header:
	ID_NAME LAYER_KEYWORD EOL
	| ID_NAME LAYER_KEYWORD extends_statement;

layer_block: layer_header layer_statements DONE_KEYWORD;

// TOKENS

IS_KEYWORD: 'is';
EXTENDS_KEYWORD: 'extends';
TOGGLE_KEYWORD: 'toggle';
LAYER_KEYWORD: 'layer';
WHEN_KEYWORD: 'when';
WITH_KEYWORD: 'with';
AND_KEYWORD: 'and';
SWAP_KEYWORD: 'swap';
BUNDLE_KEYWORD: 'bundle';
DONE_KEYWORD: 'done';
NOTHING_KEYWORD: 'nothing';

KEY_NAME:
	'caps_lock'
	| 'left_control'
	| 'left_shift'
	| 'left_option'
	| 'left_command'
	| 'right_control'
	| 'right_shift'
	| 'right_option'
	| 'right_command'
	| 'fn'
	| 'return_or_enter'
	| 'escape'
	| 'delete_or_backspace'
	| 'delete_forward'
	| 'tab'
	| 'spacebar'
	| 'hyphen'
	| 'equal_sign'
	| 'open_bracket'
	| 'close_bracket'
	| 'backslash'
	| 'non_us_pound'
	| 'semicolon'
	| 'quote'
	| 'grave_accent_and_tilde'
	| 'comma'
	| 'period'
	| 'slash'
	| 'non_us_backslash'
	| 'up_arrow'
	| 'down_arrow'
	| 'left_arrow'
	| 'right_arrow'
	| 'page_up'
	| 'page_down'
	| 'home'
	| 'end'
	| 'a'
	| 'b'
	| 'c'
	| 'd'
	| 'e'
	| 'f'
	| 'g'
	| 'h'
	| 'i'
	| 'j'
	| 'k'
	| 'l'
	| 'm'
	| 'n'
	| 'o'
	| 'p'
	| 'q'
	| 'r'
	| 's'
	| 't'
	| 'u'
	| 'v'
	| 'w'
	| 'x'
	| 'y'
	| 'z'
	| '1'
	| '2'
	| '3'
	| '4'
	| '5'
	| '6'
	| '7'
	| '8'
	| '9'
	| '0'
	| 'f1'
	| 'f2'
	| 'f3'
	| 'f4'
	| 'f5'
	| 'f6'
	| 'f7'
	| 'f8'
	| 'f9'
	| 'f10'
	| 'f11'
	| 'f12'
	| 'f13'
	| 'f14'
	| 'f15'
	| 'f16'
	| 'f17'
	| 'f18'
	| 'f19'
	| 'f20'
	| 'f21'
	| 'f22'
	| 'f23'
	| 'f24'
	| 'display_brightness_decrement'
	| 'display_brightness_increment'
	| 'mission_control'
	| 'launchpad'
	| 'dashboard'
	| 'illumination_decrement'
	| 'illumination_increment'
	| 'rewind'
	| 'play_or_pause'
	| 'fastforward'
	| 'mute'
	| 'volume_decrement'
	| 'volume_increment'
	| 'eject'
	| 'apple_display_brightness_decrement'
	| 'apple_display_brightness_increment'
	| 'apple_top_case_display_brightness_decrement'
	| 'apple_top_case_display_brightness_increment'
	| 'keypad_num_lock'
	| 'keypad_slash'
	| 'keypad_asterisk'
	| 'keypad_hyphen'
	| 'keypad_plus'
	| 'keypad_enter'
	| 'keypad_1'
	| 'keypad_2'
	| 'keypad_3'
	| 'keypad_4'
	| 'keypad_5'
	| 'keypad_6'
	| 'keypad_7'
	| 'keypad_8'
	| 'keypad_9'
	| 'keypad_0'
	| 'keypad_period'
	| 'keypad_equal_sign'
	| 'keypad_comma'
	| 'vk_none'
	| 'print_screen'
	| 'scroll_lock'
	| 'pause'
	| 'insert'
	| 'application'
	| 'help'
	| 'power'
	| 'execute'
	| 'menu'
	| 'select'
	| 'stop'
	| 'again'
	| 'undo'
	| 'cut'
	| 'copy'
	| 'paste'
	| 'find'
	| 'international1'
	| 'international2'
	| 'international3'
	| 'international4'
	| 'international5'
	| 'international6'
	| 'international7'
	| 'international8'
	| 'international9'
	| 'lang1'
	| 'lang2'
	| 'lang3'
	| 'lang4'
	| 'lang5'
	| 'lang6'
	| 'lang7'
	| 'lang8'
	| 'lang9'
	| 'japanese_eisuu'
	| 'japanese_kana'
	| 'japanese_pc_nfer'
	| 'japanese_pc_xfer'
	| 'japanese_pc_katakana'
	| 'keypad_equal_sign_as400'
	| 'locking_caps_lock'
	| 'locking_num_lock'
	| 'locking_scroll_lock'
	| 'alternate_erase'
	| 'sys_req_or_attention'
	| 'cancel'
	| 'clear'
	| 'prior'
	| 'return'
	| 'separator'
	| 'out'
	| 'oper'
	| 'clear_or_again'
	| 'cr_sel_or_props'
	| 'ex_sel'
	| 'left_alt'
	| 'left_gui'
	| 'right_alt'
	| 'right_gui'
	| 'vk_consumer_brightness_down'
	| 'vk_consumer_brightness_up'
	| 'vk_mission_control'
	| 'vk_launchpad'
	| 'vk_dashboard'
	| 'vk_consumer_illumination_down'
	| 'vk_consumer_illumination_up'
	| 'vk_consumer_previous'
	| 'vk_consumer_play'
	| 'vk_consumer_next'
	| 'volume_down'
	| 'volume_up';

ID_NAME: [a-zA-Z0-9]+;

EOL: '\r\n' | '\r' | '\n';

STRING: ~[ \r\n\t]+;

WS: [\t ]+ -> skip;
