














using System.Collections.Generic;

#pragma warning disable 1591

namespace WinDoControls
{




    public enum FontIcons
    {
        #region iconfont    English:iconfont

        #region 费用图标
        /// <summary>
        /// 收起
        /// </summary>
        I_full_screen_x = 0xe826,
        /// <summary>
        /// 全屏
        /// </summary>
        I_full_screen = 0xe824,

        /// <summary>
        /// 流转到计划设计
        /// </summary>
        I_to_plan = 0xe64d,

        /// <summary>
        /// 暂无数据 
        /// </summary>
        I_empty = 0xe6c5,

        /// <summary>
        /// 默认图片
        /// </summary>
        I_defult_img = 0xe695,


        /// <summary>
        /// 催收
        /// </summary>
        I_collection = 0xe6da,

        /// <summary>
        /// 查看单据
        /// </summary>
        I_charge_doc = 0xe6db,

        /// <summary>
        /// 查看医嘱
        /// </summary>
        //I_danjuguanli

        //忽略预警
        I_ignore_warning = 0xe6ea,

        /// <summary>
        /// 治疗实施
        /// </summary>
        I_zhiliaoshishi = 0xe65f,
        //添加备注
        I_add_remark = 0xe671,
        //处理
        I_process = 0xe6e7,
        //忽略
        I_ignore = 0xe6d9,
        //取消忽略
        I_ignore_cancel = 0xe6e5,
        //审核
        I_approve_charge = 0xe663,
        //弃审
        I_cancel_charge = 0xe652,

        //退费
        I_tuifei = 0xe655,
        //返回
        I_fanhui = 0xe6d2,


        //待选择
        I_daixuanze = 0xe6e1,
        #endregion
        /// <summary>
        /// 暂停
        /// </summary>
        I_pause = 0xe6f3,
        /// <summary>
        /// 感叹号
        /// </summary>
        I_warning2 = 0xe6f1,
        /// <summary>
        /// 转移
        /// </summary>
        I_transfer=0xe6f2,

        /// <summary>
        /// 清除代码
        /// </summary>
        I_clear_code = 0xe6ee,
        I_upload = 0xe604,
        I_user = 0xe620,
        /// <summary>
        /// 警告图标
        /// </summary>
        I_new_warning = 0xe6eb,
        /// <summary>
        /// 统计分析
        /// </summary>
        I_analysis = 0xe6e3,
        /// <summary>
        /// 立即运行
        /// </summary>
        I_start = 0xeb46,
        /// <summary>
        /// 返回
        /// </summary>
        I_back_c = 0xe6d2,
        /// <summary>
        /// 代码
        /// </summary>
        I_code = 0xe6ec,
        /// <summary>
        /// 更多
        /// </summary>
        I_more = 0xe6ce,
        /// <summary>
        /// 减号
        /// </summary>
        I_minus = 0xe6c6,

        /// <summary>
        /// 审核不通过
        /// </summary>
        I_shbtg = 0xe6b0,

        /// <summary>
        /// 审核通过
        /// </summary>
        I_shtg = 0xe6b1,

        /// <summary>
        /// 勾画开发
        /// </summary>
        I_ghys = 0xe764,

        /// <summary>
        /// 开发菜单图标
        /// </summary>
        I_doctor_icon = 0xe6f0,


        /// <summary>
        /// 后装列表
        /// </summary>
        I_hzlb = 0xe6b8,

        /// <summary>
        /// 隐藏
        /// </summary>
        I_hide = 0xe704,

        /// <summary>
        /// 激活
        /// </summary>
        I_jihuo = 0xe6c9,

        /// <summary>
        /// 通用编辑
        /// </summary>
        I_bianji = 0xe6c8,

        /// <summary>
        /// 靶区勾画
        /// </summary>
        I_bqgh = 0xe669,

        /// <summary>
        /// 自动勾画
        /// </summary>
        I_zdgh = 0xe6ae,

        /// <summary>
        /// 领取计划
        /// </summary>
        I_lqjh = 0xe6fb,

        /// <summary>
        /// 计划待评估
        /// </summary>
        I_dpg = 0xe6b3,

        /// <summary>
        /// 处方签字
        /// </summary>
        I_cfqz = 0xe6b4,

        /// <summary>
        /// 下一位
        /// </summary>
        I_next_user = 0xe6c1,
        /// <summary>
        /// 报到
        /// </summary>
        I_checkin = 0xe61f,
        /// <summary>
        /// 回形针
        /// </summary>
        I_clip = 0xe6c2,
        /// <summary>
        /// 过号
        /// </summary>
        I_overtime = 0xe6c0,
        I_info = 0xe602,
        I_money = 0xe632,
        /// <summary>
        /// 费用
        /// </summary>         
        I_charge = 0xe653,
        I_charge_clear = 0xe655,
        I_refresh = 0xe621,
        I_refresh_2 = 0xe62a,
        I_exit = 0xe675,
        I_logoff = 0xe67b,
        I_sound = 0xe629,
        I_edit_bold = 0xe651,

        /// <summary>
        /// 导出
        /// </summary>
        I_export = 0xe6c3,

        /// <summary>
        /// 剪刀
        /// </summary>
        I_jiandao = 0xe6a8,


        /// <summary>
        /// Review终止
        /// </summary>
        I_radiate_stop = 0xe6e4,

        I_send_sms = 0xe6b9,

        /// <summary>
        /// 导入用户签名
        /// </summary>
        I_import_sign = 0xe6bf,
        /// <summary>
        /// 我的
        /// </summary>
        I_my = 0xe6bc,
        /// <summary>
        /// 我组
        /// </summary>
        I_mygroup = 0xe7b2,

        /// <summary>
        /// N/A
        /// </summary>
        I_NA = 0xe6a7,
        /// <summary>
        /// 查询条件
        /// </summary>
        I_query_cdi = 0xe674,

        /// <summary>
        /// 已关联
        /// </summary>
        I_linked = 0xe6a9,
        /// <summary>
        /// 清除关联
        /// </summary>
        I_clear_link = 0xe6aa,

        I_clock_clear = 0xe688,

        /// <summary>
        /// 漏斗
        /// </summary>
        I_filter = 0xe6e9,
        /// <summary>
        /// 计划
        /// </summary>
        I_jihua = 0xe699,

        /// <summary>
        /// 关闭
        /// </summary>
        I_close_fill = 0xe6a5,

        /// <summary>
        /// 预览
        /// </summary>
        I_preview = 0xe685,

        /// <summary>
        /// 加号
        /// </summary>
        I_plus = 0xeaf3,

        /// <summary>
        /// 用户同步
        /// </summary>
        I_user_sync = 0xe60c,
        /// <summary>
        /// 撤销
        /// </summary>
        I_undo = 0xe691,

        /// <summary>
        /// 记录
        /// </summary>
        I_record = 0xe680,
        /// <summary>
        /// 手工报道
        /// </summary>
        I_handle_input = 0xe777,
        /// <summary>
        /// 日历
        /// </summary>
        I_calendar = 0xe67e,
        /// <summary>
        /// 编辑
        /// </summary>
        I_edit = 0xe608,

        I_edit_light = 0xe60e,
        /// <summary>
        /// 撤回
        /// </summary>
        I_undo_flat = 0xe603,
        I_arrow_up = 0xe636,

        /// <summary>
        /// 感叹号 三角形
        /// </summary>
        I_exclamation_delta = 0xe63f,

        /// <summary>
        /// 感叹号 三角形
        /// </summary>
        I_exclamation_delta_c = 0xe6a6,
        /// <summary>
        /// 感叹号 圆形
        /// </summary>
        I_exclamation_cycle = 0xe644,




        //儿童
        I_ertong = 0xe679,

        /// <summary>
        /// 同步
        /// </summary>
        I_sync = 0xe60c,

        /// <summary>
        /// 向下箭头
        /// </summary>
        I_arrow_down = 0xe635,
        I_remark = 0xe63a,

        I_cycle_money = 0xe62b,
        I_person_fill = 0xe676,
        I_home = 0xe648,
        I_group_of_people = 0xe64b,

        /// <summary>
        /// 设置
        /// </summary>
        I_setting_fill = 0xe6bb,
        I_setting_clear = 0xe656,

        /// <summary>
        /// 资源趋势分析
        /// </summary>
        I_zyqsfx = 0xe878,

        /// <summary>
        /// 昨日爽约人次
        /// </summary>
        I_zrsyrc = 0xe6e8,

        /// <summary>
        /// 昨日治疗人次
        /// </summary>
        I_zrzlrc = 0xe66c,

        /// <summary>
        /// 预约
        /// </summary>
        I_reserve = 0xe613,
        /// <summary>
        /// 时间
        /// </summary>
        I_time = 0xe638,
        I_reserve_fill = 0xe66e,
        I_task_fill = 0xe64e,
        I_list = 0xe628,
        I_image = 0xe752,
        I_storage = 0xea5f,
        I_toolbox = 0xe607,
        I_QA = 0xe61a,
        I_exchange = 0xe6dc,
        I_search = 0xe66f,

        /// <summary>
        /// 通知
        /// </summary>
        I_bell = 0xe6d6,
        I_bell_fill = 0xe6f6,

        /// <summary>
        /// 铃铛
        /// </summary>
        I_pen = 0xe640,

        /// <summary>
        /// 时钟
        /// </summary>
        I_clock = 0xe714,

        I_queue_number = 0xe60b,

        /// <summary>
        /// 放大
        /// </summary>
        I_magnifier = 0xe63d,

        /// <summary>
        /// 关闭
        /// </summary>
        I_close = 0xe611,

        /// <summary>
        /// 删除
        /// </summary>
        I_trash = 0xe784,

        /// <summary>
        /// 发送信息
        /// </summary>
        I_send_msg = 0xe772,
        /// <summary>
        /// 发送信息全填充
        /// </summary>
        I_send_msg_fill = 0xe612,

        I_card = 0xe62c,
        I_camera_fill = 0xe618,
        I_camera = 0xe60f,
        I_register = 0xe671,
        I_registed = 0xe609,
        I_success_fill = 0xe650,
        I_exclamation_fill = 0xe644,
        I_user_fill = 0xe647,
        I_send = 0xe646,
        I_icon03 = 0xe65f,
        I_icon11 = 0xe66d,
        I_plus_box = 0xe63c,
        I_arrow_down_fill = 0xe64c,
        I_check_face = 0xe670,

        /// <summary>
        /// 带圈对勾
        /// </summary>
        I_success = 0xe619,

        I_calender = 0xe62f,
        I_danjuguanli = 0xe6e2,
        I_jihuajindu = 0xe605,
        I_jindu = 0xe7d3,
        I_bingli = 0xe672,
        I_suifang = 0xe673,


        /// <summary>
        /// 叉号
        /// </summary>
        I_X = 0xe69d,

        /// <summary>
        /// 对勾
        /// </summary>
        I_wancheng = 0xe649,
        I_save = 0xe61b,
        I_history = 0xe638,

        /// <summary>
        /// 视图模式
        /// </summary>
        I_view_mode = 0xe665,

        /// <summary>
        /// 打印
        /// </summary>
        I_print = 0xe606,

        I_adddoc = 0xe61c,
        I_label = 0xe642,
        I_warning = 0xe654,
        I_warning_fill = 0xe60a,

        I_question = 0xe678,
        I_question_fill = 0xe610,
        I_plan = 0xe6ca,
        I_leftarrow_cycle = 0xe63e,


        I_leftarrow_clear = 0xea64,
        I_rightarrow_clear = 0xe6ab,
        I_downarrow_clear = 0xea65,

        I_leftarrow_clear2 = 0xe626,
        I_rightarrow_clear2 = 0xea63,

        /// <summary>
        /// 提交
        /// </summary>
        I_commit = 0xe63b,

        /// <summary>
        /// 作废
        /// </summary>
        I_discard = 0xe623,

        /// <summary>
        /// 复制
        /// </summary>
        I_copy = 0xe690,

        /// <summary>
        /// 设置
        /// </summary>
        I_setting = 0xe656,

        /// <summary>
        /// 右双箭头
        /// </summary>
        I_right_dobule_arrow = 0xea63,

        //上箭头 大于号
        I_arrow_up_dy = 0xea66,
        //下箭头 大于号
        I_arrow_down_dy = 0xea65,
        //发布
        I_Public=0xe6f4,
        //预览
        I_PreView2= 0xe6f7,
        //驳回至草稿箱
        I_Reject_Draft = 0xe6f8,
        //计算
        I_Calculation= 0xe6fa,
        //退款
        I_Refund =0xe6fd,
        //押金
        I_Deposit= 0xe6fe,
        #endregion
        #region Awesome    English:Awesome



        A_fa_500px = 0xf26e,



        A_fa_address_book = 0xf2b9,



        A_fa_address_book_o = 0xf2ba,



        A_fa_address_card = 0xf2bb,



        A_fa_address_card_o = 0xf2bc,



        A_fa_adjust = 0xf042,



        A_fa_adn = 0xf170,



        A_fa_align_center = 0xf037,



        A_fa_align_justify = 0xf039,



        A_fa_align_left = 0xf036,



        A_fa_align_right = 0xf038,



        A_fa_amazon = 0xf270,



        A_fa_ambulance = 0xf0f9,



        A_fa_anchor = 0xf13d,



        A_fa_android = 0xf17b,



        A_fa_angellist = 0xf209,



        A_fa_angle_double_down = 0xf103,



        A_fa_angle_double_left = 0xf100,



        A_fa_angle_double_right = 0xf101,



        A_fa_angle_double_up = 0xf102,



        A_fa_angle_down = 0xf107,



        A_fa_angle_left = 0xf104,



        A_fa_angle_right = 0xf105,



        A_fa_angle_up = 0xf106,



        A_fa_apple = 0xf179,



        A_fa_archive = 0xf187,



        A_fa_area_chart = 0xf1fe,



        A_fa_arrow_circle_down = 0xf0ab,



        A_fa_arrow_circle_left = 0xf0a8,



        A_fa_arrow_circle_o_down = 0xf01a,



        A_fa_arrow_circle_o_left = 0xf190,



        A_fa_arrow_circle_o_right = 0xf18e,



        A_fa_arrow_circle_o_up = 0xf01b,



        A_fa_arrow_circle_right = 0xf0a9,



        A_fa_arrow_circle_up = 0xf0aa,



        A_fa_arrow_down = 0xf063,



        A_fa_arrow_left = 0xf060,



        A_fa_arrow_right = 0xf061,



        A_fa_arrow_up = 0xf062,



        A_fa_arrows = 0xf047,



        A_fa_arrows_alt = 0xf0b2,



        A_fa_arrows_h = 0xf07e,



        A_fa_arrows_v = 0xf07d,



        A_fa_asl_interpreting = 0xf2a3,



        A_fa_assistive_listening_systems = 0xf2a2,



        A_fa_asterisk = 0xf069,



        A_fa_at = 0xf1fa,



        A_fa_audio_description = 0xf29e,



        A_fa_backward = 0xf04a,



        A_fa_balance_scale = 0xf24e,



        A_fa_ban = 0xf05e,



        A_fa_bandcamp = 0xf2d5,



        A_fa_bar_chart = 0xf080,



        A_fa_barcode = 0xf02a,



        A_fa_bars = 0xf0c9,



        A_fa_bath = 0xf2cd,



        A_fa_battery = 0xf240,



        A_fa_battery_0 = 0xf244,



        A_fa_battery_1 = 0xf243,



        A_fa_battery_2 = 0xf242,



        A_fa_battery_3 = 0xf241,



        A_fa_beer = 0xf0fc,



        A_fa_behance = 0xf1b4,



        A_fa_behance_square = 0xf1b5,



        A_fa_bell = 0xf0f3,



        A_fa_bell_o = 0xf0a2,



        A_fa_bell_slash = 0xf1f6,



        A_fa_bell_slash_o = 0xf1f7,



        A_fa_bicycle = 0xf206,



        A_fa_binoculars = 0xf1e5,



        A_fa_birthday_cake = 0xf1fd,



        A_fa_bitbucket = 0xf171,



        A_fa_bitbucket_square = 0xf172,



        A_fa_bitcoin = 0xf15a,



        A_fa_black_tie = 0xf27e,



        A_fa_blind = 0xf29d,



        A_fa_bluetooth = 0xf293,



        A_fa_bluetooth_b = 0xf294,



        A_fa_bold = 0xf032,



        A_fa_bomb = 0xf1e2,



        A_fa_book = 0xf02d,



        A_fa_bookmark = 0xf02e,



        A_fa_bookmark_o = 0xf097,



        A_fa_braille = 0xf2a1,



        A_fa_briefcase = 0xf0b1,



        A_fa_bug = 0xf188,



        A_fa_building = 0xf1ad,



        A_fa_building_o = 0xf0f7,



        A_fa_bullhorn = 0xf0a1,



        A_fa_bullseye = 0xf140,



        A_fa_bus = 0xf207,



        A_fa_buysellads = 0xf20d,



        A_fa_calculator = 0xf1ec,



        A_fa_calendar = 0xf073,



        A_fa_calendar_check_o = 0xf274,



        A_fa_calendar_minus_o = 0xf272,



        A_fa_calendar_o = 0xf133,



        A_fa_calendar_plus_o = 0xf271,



        A_fa_calendar_times_o = 0xf273,



        A_fa_camera = 0xf030,



        A_fa_camera_retro = 0xf083,



        A_fa_car = 0xf1b9,



        A_fa_caret_down = 0xf0d7,



        A_fa_caret_left = 0xf0d9,



        A_fa_caret_right = 0xf0da,



        A_fa_caret_up = 0xf0d8,



        A_fa_cart_arrow_down = 0xf218,



        A_fa_cart_plus = 0xf217,



        A_fa_cc = 0xf20a,



        A_fa_cc_amex = 0xf1f3,



        A_fa_cc_diners_club = 0xf24c,



        A_fa_cc_discover = 0xf1f2,



        A_fa_cc_jcb = 0xf24b,



        A_fa_cc_mastercard = 0xf1f1,



        A_fa_cc_paypal = 0xf1f4,



        A_fa_cc_stripe = 0xf1f5,



        A_fa_cc_visa = 0xf1f0,



        A_fa_certificate = 0xf0a3,



        A_fa_chain = 0xf0c1,



        A_fa_check = 0xf00c,



        A_fa_check_circle = 0xf058,



        A_fa_check_circle_o = 0xf05d,



        A_fa_check_square = 0xf14a,



        A_fa_check_square_o = 0xf046,



        A_fa_chevron_circle_down = 0xf13a,



        A_fa_chevron_circle_left = 0xf137,



        A_fa_chevron_circle_right = 0xf138,



        A_fa_chevron_circle_up = 0xf139,



        A_fa_chevron_down = 0xf078,



        A_fa_chevron_left = 0xf053,



        A_fa_chevron_right = 0xf054,



        A_fa_chevron_up = 0xf077,



        A_fa_child = 0xf1ae,



        A_fa_chrome = 0xf268,



        A_fa_circle = 0xf111,



        A_fa_circle_o = 0xf10c,



        A_fa_circle_o_notch = 0xf1ce,



        A_fa_circle_thin = 0xf1db,



        A_fa_clock_o = 0xf017,



        A_fa_clone = 0xf24d,



        A_fa_close = 0xf00d,



        A_fa_cloud = 0xf0c2,



        A_fa_cloud_download = 0xf0ed,



        A_fa_cloud_upload = 0xf0ee,



        A_fa_code = 0xf121,



        A_fa_code_fork = 0xf126,



        A_fa_codepen = 0xf1cb,



        A_fa_codiepie = 0xf284,



        A_fa_coffee = 0xf0f4,



        A_fa_cog = 0xf013,



        A_fa_cogs = 0xf085,



        A_fa_columns = 0xf0db,



        A_fa_comment = 0xf075,



        A_fa_comment_o = 0xf0e5,



        A_fa_commenting = 0xf27a,



        A_fa_commenting_o = 0xf27b,



        A_fa_comments = 0xf086,



        A_fa_comments_o = 0xf0e6,



        A_fa_compass = 0xf14e,



        A_fa_compress = 0xf066,



        A_fa_connectdevelop = 0xf20e,



        A_fa_contao = 0xf26d,



        A_fa_copy = 0xf0c5,



        A_fa_copyright = 0xf1f9,



        A_fa_creative_commons = 0xf25e,



        A_fa_credit_card = 0xf09d,



        A_fa_credit_card_alt = 0xf283,



        A_fa_crop = 0xf125,



        A_fa_crosshairs = 0xf05b,



        A_fa_css3 = 0xf13c,



        A_fa_cube = 0xf1b2,



        A_fa_cubes = 0xf1b3,



        A_fa_cut = 0xf0c4,



        A_fa_cutlery = 0xf0f5,



        A_fa_dashboard = 0xf0e4,



        A_fa_dashcube = 0xf210,



        A_fa_database = 0xf1c0,



        A_fa_deaf = 0xf2a4,



        A_fa_delicious = 0xf1a5,



        A_fa_desktop = 0xf108,



        A_fa_deviantart = 0xf1bd,



        A_fa_diamond = 0xf219,



        A_fa_digg = 0xf1a6,



        A_fa_dot_circle_o = 0xf192,



        A_fa_download = 0xf019,



        A_fa_dribbble = 0xf17d,



        A_fa_dropbox = 0xf16b,



        A_fa_drupal = 0xf1a9,



        A_fa_edge = 0xf282,



        A_fa_edit = 0xf044,



        A_fa_eercast = 0xf2da,



        A_fa_eject = 0xf052,



        A_fa_ellipsis_h = 0xf141,



        A_fa_ellipsis_v = 0xf142,



        A_fa_empire = 0xf1d1,



        A_fa_envelope = 0xf0e0,



        A_fa_envelope_o = 0xf003,



        A_fa_envelope_open = 0xf2b6,



        A_fa_envelope_open_o = 0xf2b7,



        A_fa_envelope_square = 0xf199,



        A_fa_envira = 0xf299,



        A_fa_eraser = 0xf12d,



        A_fa_etsy = 0xf2d7,



        A_fa_euro = 0xf153,



        A_fa_exchange = 0xf0ec,



        A_fa_exclamation = 0xf12a,



        A_fa_exclamation_circle = 0xf06a,



        A_fa_expand = 0xf065,



        A_fa_expeditedssl = 0xf23e,



        A_fa_external_link = 0xf08e,



        A_fa_external_link_square = 0xf14c,



        A_fa_eye = 0xf06e,



        A_fa_eye_slash = 0xf070,



        A_fa_eyedropper = 0xf1fb,



        A_fa_fa = 0xf2b4,



        A_fa_facebook = 0xf09a,



        A_fa_facebook_official = 0xf230,



        A_fa_facebook_square = 0xf082,



        A_fa_fast_backward = 0xf049,



        A_fa_fast_forward = 0xf050,



        A_fa_fax = 0xf1ac,



        A_fa_feed = 0xf09e,



        A_fa_female = 0xf182,



        A_fa_fighter_jet = 0xf0fb,



        A_fa_file = 0xf15b,



        A_fa_file_code_o = 0xf1c9,



        A_fa_file_excel_o = 0xf1c3,



        A_fa_file_o = 0xf016,



        A_fa_file_pdf_o = 0xf1c1,



        A_fa_file_photo_o = 0xf1c5,



        A_fa_file_powerpoint_o = 0xf1c4,



        A_fa_file_sound_o = 0xf1c7,



        A_fa_file_text = 0xf15c,



        A_fa_file_text_o = 0xf0f6,



        A_fa_file_video_o = 0xf1c8,



        A_fa_file_word_o = 0xf1c2,



        A_fa_file_zip_o = 0xf1c6,



        A_fa_film = 0xf008,



        A_fa_filter = 0xf0b0,



        A_fa_fire = 0xf06d,



        A_fa_fire_extinguisher = 0xf134,



        A_fa_firefox = 0xf269,



        A_fa_first_order = 0xf2b0,



        A_fa_flag = 0xf024,



        A_fa_flag_checkered = 0xf11e,



        A_fa_flag_o = 0xf11d,



        A_fa_flash = 0xf0e7,



        A_fa_flask = 0xf0c3,



        A_fa_flickr = 0xf16e,



        A_fa_folder = 0xf07b,



        A_fa_folder_o = 0xf114,



        A_fa_folder_open = 0xf07c,



        A_fa_folder_open_o = 0xf115,



        A_fa_font = 0xf031,



        A_fa_fonticons = 0xf280,



        A_fa_fort_awesome = 0xf286,



        A_fa_forumbee = 0xf211,



        A_fa_forward = 0xf04e,



        A_fa_foursquare = 0xf180,



        A_fa_free_code_camp = 0xf2c5,



        A_fa_frown_o = 0xf119,



        A_fa_futbol_o = 0xf1e3,



        A_fa_gamepad = 0xf11b,



        A_fa_gbp = 0xf154,



        A_fa_genderless = 0xf22d,



        A_fa_get_pocket = 0xf265,



        A_fa_gg = 0xf260,



        A_fa_gg_circle = 0xf261,



        A_fa_gift = 0xf06b,



        A_fa_git = 0xf1d3,



        A_fa_git_square = 0xf1d2,



        A_fa_github = 0xf09b,



        A_fa_github_alt = 0xf113,



        A_fa_github_square = 0xf092,



        A_fa_gitlab = 0xf296,



        A_fa_gittip = 0xf184,



        A_fa_glass = 0xf000,



        A_fa_glide = 0xf2a5,



        A_fa_glide_g = 0xf2a6,



        A_fa_globe = 0xf0ac,



        A_fa_google = 0xf1a0,



        A_fa_google_plus = 0xf0d5,



        A_fa_google_plus_official = 0xf2b3,



        A_fa_google_plus_square = 0xf0d4,



        A_fa_google_wallet = 0xf1ee,



        A_fa_graduation_cap = 0xf19d,



        A_fa_grav = 0xf2d6,



        A_fa_group = 0xf0c0,



        A_fa_h_square = 0xf0fd,



        A_fa_hacker_news = 0xf1d4,



        A_fa_hand_lizard_o = 0xf258,



        A_fa_hand_o_down = 0xf0a7,



        A_fa_hand_o_left = 0xf0a5,



        A_fa_hand_o_right = 0xf0a4,



        A_fa_hand_o_up = 0xf0a6,



        A_fa_hand_peace_o = 0xf25b,



        A_fa_hand_pointer_o = 0xf25a,



        A_fa_hand_rock_o = 0xf255,



        A_fa_hand_scissors_o = 0xf257,



        A_fa_hand_spock_o = 0xf259,



        A_fa_hand_stop_o = 0xf256,



        A_fa_handshake_o = 0xf2b5,



        A_fa_hashtag = 0xf292,



        A_fa_hdd_o = 0xf0a0,



        A_fa_header = 0xf1dc,



        A_fa_headphones = 0xf025,



        A_fa_heart = 0xf004,



        A_fa_heart_o = 0xf08a,



        A_fa_heartbeat = 0xf21e,



        A_fa_history = 0xf1da,



        A_fa_home = 0xf015,



        A_fa_hospital_o = 0xf0f8,



        A_fa_hotel = 0xf236,



        A_fa_hourglass = 0xf254,



        A_fa_hourglass_end = 0xf253,



        A_fa_hourglass_half = 0xf252,



        A_fa_hourglass_o = 0xf250,



        A_fa_hourglass_start = 0xf251,



        A_fa_houzz = 0xf27c,



        A_fa_html5 = 0xf13b,



        A_fa_i_cursor = 0xf246,



        A_fa_id_badge = 0xf2c1,



        A_fa_id_card = 0xf2c2,



        A_fa_id_card_o = 0xf2c3,



        A_fa_imdb = 0xf2d8,



        A_fa_inbox = 0xf01c,



        A_fa_indent = 0xf03c,



        A_fa_industry = 0xf275,



        A_fa_info = 0xf129,



        A_fa_info_circle = 0xf05a,



        A_fa_inr = 0xf156,



        A_fa_instagram = 0xf16d,



        A_fa_internet_explorer = 0xf26b,



        A_fa_ioxhost = 0xf208,



        A_fa_italic = 0xf033,



        A_fa_joomla = 0xf1aa,



        A_fa_jsfiddle = 0xf1cc,



        A_fa_key = 0xf084,



        A_fa_keyboard_o = 0xf11c,



        A_fa_language = 0xf1ab,



        A_fa_laptop = 0xf109,



        A_fa_lastfm = 0xf202,



        A_fa_lastfm_square = 0xf203,



        A_fa_leaf = 0xf06c,



        A_fa_leanpub = 0xf212,



        A_fa_legal = 0xf0e3,



        A_fa_lemon_o = 0xf094,



        A_fa_level_down = 0xf149,



        A_fa_level_up = 0xf148,



        A_fa_life_buoy = 0xf1cd,



        A_fa_lightbulb_o = 0xf0eb,



        A_fa_line_chart = 0xf201,



        A_fa_linkedin = 0xf0e1,



        A_fa_linkedin_square = 0xf08c,



        A_fa_linode = 0xf2b8,



        A_fa_linux = 0xf17c,



        A_fa_list = 0xf03a,



        A_fa_list_alt = 0xf022,



        A_fa_list_ol = 0xf0cb,



        A_fa_list_ul = 0xf0ca,



        A_fa_location_arrow = 0xf124,



        A_fa_lock = 0xf023,



        A_fa_long_arrow_down = 0xf175,



        A_fa_long_arrow_left = 0xf177,



        A_fa_long_arrow_right = 0xf178,



        A_fa_long_arrow_up = 0xf176,



        A_fa_low_vision = 0xf2a8,



        A_fa_magic = 0xf0d0,



        A_fa_magnet = 0xf076,



        A_fa_mail_forward = 0xf064,



        A_fa_male = 0xf183,



        A_fa_map = 0xf279,



        A_fa_map_marker = 0xf041,



        A_fa_map_o = 0xf278,



        A_fa_map_pin = 0xf276,



        A_fa_map_signs = 0xf277,



        A_fa_mars = 0xf222,



        A_fa_mars_double = 0xf227,



        A_fa_mars_stroke = 0xf229,



        A_fa_mars_stroke_h = 0xf22b,



        A_fa_mars_stroke_v = 0xf22a,



        A_fa_maxcdn = 0xf136,



        A_fa_meanpath = 0xf20c,



        A_fa_medium = 0xf23a,



        A_fa_medkit = 0xf0fa,



        A_fa_meetup = 0xf2e0,



        A_fa_meh_o = 0xf11a,



        A_fa_mercury = 0xf223,



        A_fa_microchip = 0xf2db,



        A_fa_microphone = 0xf130,



        A_fa_microphone_slash = 0xf131,



        A_fa_minus = 0xf068,



        A_fa_minus_circle = 0xf056,



        A_fa_minus_square = 0xf146,



        A_fa_minus_square_o = 0xf147,



        A_fa_mixcloud = 0xf289,



        A_fa_mobile_phone = 0xf10b,



        A_fa_modx = 0xf285,



        A_fa_money = 0xf0d6,



        A_fa_moon_o = 0xf186,



        A_fa_motorcycle = 0xf21c,



        A_fa_mouse_pointer = 0xf245,



        A_fa_music = 0xf001,



        A_fa_neuter = 0xf22c,



        A_fa_newspaper_o = 0xf1ea,



        A_fa_object_group = 0xf247,



        A_fa_object_ungroup = 0xf248,



        A_fa_odnoklassniki = 0xf263,



        A_fa_odnoklassniki_square = 0xf264,



        A_fa_opencart = 0xf23d,



        A_fa_openid = 0xf19b,



        A_fa_opera = 0xf26a,



        A_fa_optin_monster = 0xf23c,



        A_fa_outdent = 0xf03b,



        A_fa_pagelines = 0xf18c,



        A_fa_paint_brush = 0xf1fc,



        A_fa_paperclip = 0xf0c6,



        A_fa_paragraph = 0xf1dd,



        A_fa_paste = 0xf0ea,



        A_fa_pause = 0xf04c,



        A_fa_pause_circle = 0xf28b,



        A_fa_pause_circle_o = 0xf28c,



        A_fa_paw = 0xf1b0,



        A_fa_paypal = 0xf1ed,



        A_fa_pencil = 0xf040,



        A_fa_pencil_square = 0xf14b,



        A_fa_percent = 0xf295,



        A_fa_phone = 0xf095,



        A_fa_phone_square = 0xf098,



        A_fa_photo = 0xf03e,



        A_fa_pie_chart = 0xf200,



        A_fa_pied_piper = 0xf2ae,



        A_fa_pied_piper_alt = 0xf1a8,



        A_fa_pied_piper_pp = 0xf1a7,



        A_fa_pinterest = 0xf0d2,



        A_fa_pinterest_p = 0xf231,



        A_fa_pinterest_square = 0xf0d3,



        A_fa_plane = 0xf072,



        A_fa_play = 0xf04b,



        A_fa_play_circle = 0xf144,



        A_fa_play_circle_o = 0xf01d,



        A_fa_plug = 0xf1e6,



        A_fa_plus = 0xf067,



        A_fa_plus_circle = 0xf055,



        A_fa_plus_square = 0xf0fe,



        A_fa_plus_square_o = 0xf196,



        A_fa_podcast = 0xf2ce,



        A_fa_power_off = 0xf011,



        A_fa_print = 0xf02f,



        A_fa_product_hunt = 0xf288,



        A_fa_puzzle_piece = 0xf12e,



        A_fa_qq = 0xf1d6,



        A_fa_qrcode = 0xf029,



        A_fa_question = 0xf128,



        A_fa_question_circle = 0xf059,



        A_fa_question_circle_o = 0xf29c,



        A_fa_quora = 0xf2c4,



        A_fa_quote_left = 0xf10d,



        A_fa_quote_right = 0xf10e,



        A_fa_random = 0xf074,



        A_fa_ravelry = 0xf2d9,



        A_fa_recycle = 0xf1b8,



        A_fa_reddit = 0xf1a1,



        A_fa_reddit_alien = 0xf281,



        A_fa_reddit_square = 0xf1a2,



        A_fa_refresh = 0xf021,



        A_fa_registered = 0xf25d,



        A_fa_renren = 0xf18b,



        A_fa_reply = 0xf112,



        A_fa_reply_all = 0xf122,



        A_fa_resistance = 0xf1d0,



        A_fa_retweet = 0xf079,



        A_fa_rmb = 0xf157,



        A_fa_road = 0xf018,



        A_fa_rocket = 0xf135,



        A_fa_rotate_right = 0xf01e,



        A_fa_rss_square = 0xf143,



        A_fa_ruble = 0xf158,



        A_fa_safari = 0xf267,



        A_fa_save = 0xf0c7,



        A_fa_scribd = 0xf28a,



        A_fa_search = 0xf002,



        A_fa_search_minus = 0xf010,



        A_fa_search_plus = 0xf00e,



        A_fa_sellsy = 0xf213,



        A_fa_send = 0xf1d8,



        A_fa_send_o = 0xf1d9,



        A_fa_server = 0xf233,



        A_fa_share_alt = 0xf1e0,



        A_fa_share_alt_square = 0xf1e1,



        A_fa_share_square = 0xf14d,



        A_fa_share_square_o = 0xf045,



        A_fa_shekel = 0xf20b,



        A_fa_shield = 0xf132,



        A_fa_ship = 0xf21a,



        A_fa_shirtsinbulk = 0xf214,



        A_fa_shopping_bag = 0xf290,



        A_fa_shopping_basket = 0xf291,



        A_fa_shopping_cart = 0xf07a,



        A_fa_shower = 0xf2cc,



        A_fa_sign_in = 0xf090,



        A_fa_sign_language = 0xf2a7,



        A_fa_sign_out = 0xf08b,



        A_fa_signal = 0xf012,



        A_fa_simplybuilt = 0xf215,



        A_fa_sitemap = 0xf0e8,



        A_fa_skyatlas = 0xf216,



        A_fa_skype = 0xf17e,



        A_fa_slack = 0xf198,



        A_fa_sliders = 0xf1de,



        A_fa_slideshare = 0xf1e7,



        A_fa_smile_o = 0xf118,



        A_fa_snapchat = 0xf2ab,



        A_fa_snapchat_ghost = 0xf2ac,



        A_fa_snapchat_square = 0xf2ad,



        A_fa_snowflake_o = 0xf2dc,



        A_fa_sort = 0xf0dc,



        A_fa_sort_alpha_asc = 0xf15d,



        A_fa_sort_alpha_desc = 0xf15e,



        A_fa_sort_amount_asc = 0xf160,



        A_fa_sort_amount_desc = 0xf161,



        A_fa_sort_asc = 0xf0de,



        A_fa_sort_desc = 0xf0dd,



        A_fa_sort_numeric_asc = 0xf162,



        A_fa_sort_numeric_desc = 0xf163,



        A_fa_soundcloud = 0xf1be,



        A_fa_space_shuttle = 0xf197,



        A_fa_spinner = 0xf110,



        A_fa_spoon = 0xf1b1,



        A_fa_spotify = 0xf1bc,



        A_fa_square = 0xf0c8,



        A_fa_square_o = 0xf096,



        A_fa_stack_exchange = 0xf18d,



        A_fa_stack_overflow = 0xf16c,



        A_fa_star = 0xf005,



        A_fa_star_half = 0xf089,



        A_fa_star_half_o = 0xf123,



        A_fa_star_o = 0xf006,



        A_fa_steam = 0xf1b6,



        A_fa_steam_square = 0xf1b7,



        A_fa_step_backward = 0xf048,



        A_fa_step_forward = 0xf051,



        A_fa_stethoscope = 0xf0f1,



        A_fa_sticky_note = 0xf249,



        A_fa_sticky_note_o = 0xf24a,



        A_fa_stop = 0xf04d,



        A_fa_stop_circle = 0xf28d,



        A_fa_stop_circle_o = 0xf28e,



        A_fa_street_view = 0xf21d,



        A_fa_strikethrough = 0xf0cc,



        A_fa_stumbleupon = 0xf1a4,



        A_fa_stumbleupon_circle = 0xf1a3,



        A_fa_subscript = 0xf12c,



        A_fa_subway = 0xf239,



        A_fa_suitcase = 0xf0f2,



        A_fa_sun_o = 0xf185,



        A_fa_superpowers = 0xf2dd,



        A_fa_superscript = 0xf12b,



        A_fa_table = 0xf0ce,



        A_fa_tablet = 0xf10a,



        A_fa_tag = 0xf02b,



        A_fa_tags = 0xf02c,



        A_fa_tasks = 0xf0ae,



        A_fa_taxi = 0xf1ba,



        A_fa_telegram = 0xf2c6,



        A_fa_tencent_weibo = 0xf1d5,



        A_fa_terminal = 0xf120,



        A_fa_text_height = 0xf034,



        A_fa_text_width = 0xf035,



        A_fa_th = 0xf00a,



        A_fa_th_large = 0xf009,



        A_fa_th_list = 0xf00b,



        A_fa_themeisle = 0xf2b2,



        A_fa_thermometer = 0xf2c7,



        A_fa_thermometer_0 = 0xf2cb,



        A_fa_thermometer_1 = 0xf2ca,



        A_fa_thermometer_2 = 0xf2c9,



        A_fa_thermometer_3 = 0xf2c8,



        A_fa_thumb_tack = 0xf08d,



        A_fa_thumbs_down = 0xf165,



        A_fa_thumbs_o_down = 0xf088,



        A_fa_thumbs_o_up = 0xf087,



        A_fa_thumbs_up = 0xf164,



        A_fa_ticket = 0xf145,



        A_fa_times_circle = 0xf057,



        A_fa_times_circle_o = 0xf05c,



        A_fa_tint = 0xf043,



        A_fa_toggle_down = 0xf150,



        A_fa_toggle_left = 0xf191,



        A_fa_toggle_off = 0xf204,



        A_fa_toggle_on = 0xf205,



        A_fa_toggle_right = 0xf152,



        A_fa_toggle_up = 0xf151,



        A_fa_trademark = 0xf25c,



        A_fa_train = 0xf238,



        A_fa_transgender = 0xf224,



        A_fa_transgender_alt = 0xf225,



        A_fa_trash = 0xf1f8,



        A_fa_trash_o = 0xf014,



        A_fa_tree = 0xf1bb,



        A_fa_trello = 0xf181,



        A_fa_tripadvisor = 0xf262,



        A_fa_trophy = 0xf091,



        A_fa_truck = 0xf0d1,



        A_fa_tty = 0xf1e4,



        A_fa_tumblr = 0xf173,



        A_fa_tumblr_square = 0xf174,



        A_fa_turkish_lira = 0xf195,



        A_fa_tv = 0xf26c,



        A_fa_twitch = 0xf1e8,



        A_fa_twitter = 0xf099,



        A_fa_twitter_square = 0xf081,



        A_fa_umbrella = 0xf0e9,



        A_fa_underline = 0xf0cd,



        A_fa_undo = 0xf0e2,



        A_fa_universal_access = 0xf29a,



        A_fa_university = 0xf19c,



        A_fa_unlink = 0xf127,



        A_fa_unlock = 0xf09c,



        A_fa_unlock_alt = 0xf13e,



        A_fa_upload = 0xf093,



        A_fa_usb = 0xf287,



        A_fa_usd = 0xf155,



        A_fa_user = 0xf007,



        A_fa_user_circle = 0xf2bd,



        A_fa_user_circle_o = 0xf2be,



        A_fa_user_md = 0xf0f0,



        A_fa_user_o = 0xf2c0,



        A_fa_user_plus = 0xf234,



        A_fa_user_secret = 0xf21b,



        A_fa_user_times = 0xf235,



        A_fa_venus = 0xf221,



        A_fa_venus_double = 0xf226,



        A_fa_venus_mars = 0xf228,



        A_fa_viacoin = 0xf237,



        A_fa_viadeo = 0xf2a9,



        A_fa_viadeo_square = 0xf2aa,



        A_fa_video_camera = 0xf03d,



        A_fa_vimeo = 0xf27d,



        A_fa_vimeo_square = 0xf194,



        A_fa_vine = 0xf1ca,



        A_fa_vk = 0xf189,



        A_fa_volume_control_phone = 0xf2a0,



        A_fa_volume_down = 0xf027,



        A_fa_volume_off = 0xf026,



        A_fa_volume_up = 0xf028,



        A_fa_warning = 0xf071,



        A_fa_weibo = 0xf18a,



        A_fa_weixin = 0xf1d7,



        A_fa_whatsapp = 0xf232,



        A_fa_wheelchair = 0xf193,



        A_fa_wheelchair_alt = 0xf29b,



        A_fa_wifi = 0xf1eb,



        A_fa_wikipedia_w = 0xf266,



        A_fa_window_close = 0xf2d3,



        A_fa_window_close_o = 0xf2d4,



        A_fa_window_maximize = 0xf2d0,



        A_fa_window_minimize = 0xf2d1,



        A_fa_window_restore = 0xf2d2,



        A_fa_windows = 0xf17a,



        A_fa_won = 0xf159,



        A_fa_wordpress = 0xf19a,



        A_fa_wpbeginner = 0xf297,



        A_fa_wpexplorer = 0xf2de,



        A_fa_wpforms = 0xf298,



        A_fa_wrench = 0xf0ad,



        A_fa_xing = 0xf168,



        A_fa_xing_square = 0xf169,



        A_fa_yahoo = 0xf19e,



        A_fa_yc = 0xf23b,



        A_fa_yelp = 0xf1e9,



        A_fa_yoast = 0xf2b1,



        A_fa_youtube = 0xf167,



        A_fa_youtube_play = 0xf16a,



        A_fa_youtube_square = 0xf166,
        #endregion

        #region Elegant    English:Elegant



        E_arrow_up = 0x21,



        E_arrow_down = 0x22,



        E_arrow_left = 0x23,



        E_arrow_right = 0x24,



        E_arrow_left_up = 0x25,



        E_arrow_right_up = 0x26,



        E_arrow_right_down = 0x27,



        E_arrow_left_down = 0x28,



        E_arrow_up_down = 0x29,



        E_arrow_up_down_alt = 0x2a,



        E_arrow_left_right_alt = 0x2b,



        E_arrow_left_right = 0x2c,



        E_arrow_expand_alt2 = 0x2d,



        E_arrow_expand_alt = 0x2e,



        E_arrow_condense = 0x2f,



        E_arrow_expand = 0x30,



        E_arrow_move = 0x31,



        E_arrow_carrot_up = 0x32,



        E_arrow_carrot_down = 0x33,



        E_arrow_carrot_left = 0x34,



        E_arrow_carrot_right = 0x35,



        E_arrow_carrot_2up = 0x36,



        E_arrow_carrot_2down = 0x37,



        E_arrow_carrot_2left = 0x38,



        E_arrow_carrot_2right = 0x39,



        E_arrow_carrot_up_alt2 = 0x3a,



        E_arrow_carrot_down_alt2 = 0x3b,



        E_arrow_carrot_left_alt2 = 0x3c,



        E_arrow_carrot_right_alt2 = 0x3d,



        E_arrow_carrot_2up_alt2 = 0x3e,



        E_arrow_carrot_2down_alt2 = 0x3f,



        E_arrow_carrot_2left_alt2 = 0x40,



        E_arrow_carrot_2right_alt2 = 0x41,



        E_arrow_triangle_up = 0x42,



        E_arrow_triangle_down = 0x43,



        E_arrow_triangle_left = 0x44,



        E_arrow_triangle_right = 0x45,



        E_arrow_triangle_up_alt2 = 0x46,



        E_arrow_triangle_down_alt2 = 0x47,



        E_arrow_triangle_left_alt2 = 0x48,



        E_arrow_triangle_right_alt2 = 0x49,



        E_arrow_back = 0x4a,



        E_icon_minus_06 = 0x4b,



        E_icon_plus = 0x4c,



        E_icon_close = 0x4d,



        E_icon_check = 0x4e,



        E_icon_minus_alt2 = 0x4f,



        E_icon_plus_alt2 = 0x50,



        E_icon_close_alt2 = 0x51,



        E_icon_check_alt2 = 0x52,



        E_icon_zoom_out_alt = 0x53,



        E_icon_zoom_in_alt = 0x54,



        E_icon_search = 0x55,



        E_icon_box_empty = 0x56,



        E_icon_box_selected = 0x57,



        E_icon_minus_box = 0x58,



        E_icon_plus_box = 0x59,



        E_icon_box_checked = 0x5a,



        E_icon_circle_empty = 0x5b,



        E_icon_circle_slelected = 0x5c,



        E_icon_stop_alt2 = 0x5d,



        E_icon_stop = 0x5e,



        E_icon_pause_alt2 = 0x5f,



        E_icon_pause = 0x60,



        E_icon_menu = 0x61,



        E_icon_menu_square_alt2 = 0x62,



        E_icon_menu_circle_alt2 = 0x63,



        E_icon_ul = 0x64,



        E_icon_ol = 0x65,



        E_icon_adjust_horiz = 0x66,



        E_icon_adjust_vert = 0x67,



        E_icon_document_alt = 0x68,



        E_icon_documents_alt = 0x69,



        E_icon_pencil = 0x6a,



        E_icon_pencil_edit_alt = 0x6b,



        E_icon_pencil_edit = 0x6c,



        E_icon_folder_alt = 0x6d,



        E_icon_folder_open_alt = 0x6e,



        E_icon_folder_add_alt = 0x6f,



        E_icon_info_alt = 0x70,



        E_icon_error_oct_alt = 0x71,



        E_icon_error_circle_alt = 0x72,



        E_icon_error_triangle_alt = 0x73,



        E_icon_question_alt2 = 0x74,



        E_icon_question = 0x75,



        E_icon_comment_alt = 0x76,



        E_icon_chat_alt = 0x77,



        E_icon_vol_mute_alt = 0x78,



        E_icon_volume_low_alt = 0x79,



        E_icon_volume_high_alt = 0x7a,



        E_icon_quotations = 0x7b,



        E_icon_quotations_alt2 = 0x7c,



        E_icon_clock_alt = 0x7d,



        E_icon_lock_alt = 0x7e,



        E_icon_lock_open_alt = 0xe000,



        E_icon_key_alt = 0xe001,



        E_icon_cloud_alt = 0xe002,



        E_icon_cloud_upload_alt = 0xe003,



        E_icon_cloud_download_alt = 0xe004,



        E_icon_image = 0xe005,



        E_icon_images = 0xe006,



        E_icon_lightbulb_alt = 0xe007,



        E_icon_gift_alt = 0xe008,



        E_icon_house_alt = 0xe009,



        E_icon_genius = 0xe00a,



        E_icon_mobile = 0xe00b,



        E_icon_tablet = 0xe00c,



        E_icon_laptop = 0xe00d,



        E_icon_desktop = 0xe00e,



        E_icon_camera_alt = 0xe00f,



        E_icon_mail_alt = 0xe010,



        E_icon_cone_alt = 0xe011,



        E_icon_ribbon_alt = 0xe012,



        E_icon_bag_alt = 0xe013,



        E_icon_creditcard = 0xe014,



        E_icon_cart_alt = 0xe015,



        E_icon_paperclip = 0xe016,



        E_icon_tag_alt = 0xe017,



        E_icon_tags_alt = 0xe018,



        E_icon_trash_alt = 0xe019,



        E_icon_cursor_alt = 0xe01a,



        E_icon_mic_alt = 0xe01b,



        E_icon_compass_alt = 0xe01c,



        E_icon_pin_alt = 0xe01d,



        E_icon_pushpin_alt = 0xe01e,



        E_icon_map_alt = 0xe01f,



        E_icon_drawer_alt = 0xe020,



        E_icon_toolbox_alt = 0xe021,



        E_icon_book_alt = 0xe022,



        E_icon_calendar = 0xe023,



        E_icon_film = 0xe024,



        E_icon_table = 0xe025,



        E_icon_contacts_alt = 0xe026,



        E_icon_headphones = 0xe027,



        E_icon_lifesaver = 0xe028,



        E_icon_piechart = 0xe029,



        E_icon_refresh = 0xe02a,



        E_icon_link_alt = 0xe02b,



        E_icon_link = 0xe02c,



        E_icon_loading = 0xe02d,



        E_icon_blocked = 0xe02e,



        E_icon_archive_alt = 0xe02f,



        E_icon_heart_alt = 0xe030,



        E_icon_star_alt = 0xe031,



        E_icon_star_half_alt = 0xe032,



        E_icon_star = 0xe033,



        E_icon_star_half = 0xe034,



        E_icon_tools = 0xe035,



        E_icon_tool = 0xe036,



        E_icon_cog = 0xe037,



        E_icon_cogs = 0xe038,



        E_arrow_up_alt = 0xe039,



        E_arrow_down_alt = 0xe03a,



        E_arrow_left_alt = 0xe03b,



        E_arrow_right_alt = 0xe03c,



        E_arrow_left_up_alt = 0xe03d,



        E_arrow_right_up_alt = 0xe03e,



        E_arrow_right_down_alt = 0xe03f,



        E_arrow_left_down_alt = 0xe040,



        E_arrow_condense_alt = 0xe041,



        E_arrow_expand_alt3 = 0xe042,



        E_arrow_carrot_up_alt = 0xe043,



        E_arrow_carrot_down_alt = 0xe044,



        E_arrow_carrot_left_alt = 0xe045,



        E_arrow_carrot_right_alt = 0xe046,



        E_arrow_carrot_2up_alt = 0xe047,



        E_arrow_carrot_2dwnn_alt = 0xe048,



        E_arrow_carrot_2left_alt = 0xe049,



        E_arrow_carrot_2right_alt = 0xe04a,



        E_arrow_triangle_up_alt = 0xe04b,



        E_arrow_triangle_down_alt = 0xe04c,



        E_arrow_triangle_left_alt = 0xe04d,



        E_arrow_triangle_right_alt = 0xe04e,



        E_icon_minus_alt = 0xe04f,



        E_icon_plus_alt = 0xe050,



        E_icon_close_alt = 0xe051,



        E_icon_check_alt = 0xe052,



        E_icon_zoom_out = 0xe053,



        E_icon_zoom_in = 0xe054,



        E_icon_stop_alt = 0xe055,



        E_icon_menu_square_alt = 0xe056,



        E_icon_menu_circle_alt = 0xe057,



        E_icon_document = 0xe058,



        E_icon_documents = 0xe059,



        E_icon_pencil_alt = 0xe05a,



        E_icon_folder = 0xe05b,



        E_icon_folder_open = 0xe05c,



        E_icon_folder_add = 0xe05d,



        E_icon_folder_upload = 0xe05e,



        E_icon_folder_download = 0xe05f,



        E_icon_info = 0xe060,



        E_icon_error_circle = 0xe061,



        E_icon_error_oct = 0xe062,



        E_icon_error_triangle = 0xe063,



        E_icon_question_alt = 0xe064,



        E_icon_comment = 0xe065,



        E_icon_chat = 0xe066,



        E_icon_vol_mute = 0xe067,



        E_icon_volume_low = 0xe068,



        E_icon_volume_high = 0xe069,



        E_icon_quotations_alt = 0xe06a,



        E_icon_clock = 0xe06b,



        E_icon_lock = 0xe06c,



        E_icon_lock_open = 0xe06d,



        E_icon_key = 0xe06e,



        E_icon_cloud = 0xe06f,



        E_icon_cloud_upload = 0xe070,



        E_icon_cloud_download = 0xe071,



        E_icon_lightbulb = 0xe072,



        E_icon_gift = 0xe073,



        E_icon_house = 0xe074,



        E_icon_camera = 0xe075,



        E_icon_mail = 0xe076,



        E_icon_cone = 0xe077,



        E_icon_ribbon = 0xe078,



        E_icon_bag = 0xe079,



        E_icon_cart = 0xe07a,



        E_icon_tag = 0xe07b,



        E_icon_tags = 0xe07c,



        E_icon_trash = 0xe07d,



        E_icon_cursor = 0xe07e,



        E_icon_mic = 0xe07f,



        E_icon_compass = 0xe080,



        E_icon_pin = 0xe081,



        E_icon_pushpin = 0xe082,



        E_icon_map = 0xe083,



        E_icon_drawer = 0xe084,



        E_icon_toolbox = 0xe085,



        E_icon_book = 0xe086,



        E_icon_contacts = 0xe087,



        E_icon_archive = 0xe088,



        E_icon_heart = 0xe089,



        E_icon_profile = 0xe08a,



        E_icon_group = 0xe08b,



        E_icon_grid_2x2 = 0xe08c,



        E_icon_grid_3x3 = 0xe08d,



        E_icon_music = 0xe08e,



        E_icon_pause_alt = 0xe08f,



        E_icon_phone = 0xe090,



        E_icon_upload = 0xe091,



        E_icon_download = 0xe092,



        E_social_facebook = 0xe093,



        E_social_twitter = 0xe094,



        E_social_pinterest = 0xe095,



        E_social_googleplus = 0xe096,



        E_social_tumblr = 0xe097,



        E_social_tumbleupon = 0xe098,



        E_social_wordpress = 0xe099,



        E_social_instagram = 0xe09a,



        E_social_dribbble = 0xe09b,



        E_social_vimeo = 0xe09c,



        E_social_linkedin = 0xe09d,



        E_social_rss = 0xe09e,



        E_social_deviantart = 0xe09f,



        E_social_share = 0xe0a0,



        E_social_myspace = 0xe0a1,



        E_social_skype = 0xe0a2,



        E_social_youtube = 0xe0a3,



        E_social_picassa = 0xe0a4,



        E_social_googledrive = 0xe0a5,



        E_social_flickr = 0xe0a6,



        E_social_blogger = 0xe0a7,



        E_social_spotify = 0xe0a8,



        E_social_delicious = 0xe0a9,



        E_social_facebook_circle = 0xe0aa,



        E_social_twitter_circle = 0xe0ab,



        E_social_pinterest_circle = 0xe0ac,



        E_social_googleplus_circle = 0xe0ad,



        E_social_tumblr_circle = 0xe0ae,



        E_social_stumbleupon_circle = 0xe0af,



        E_social_wordpress_circle = 0xe0b0,



        E_social_instagram_circle = 0xe0b1,



        E_social_dribbble_circle = 0xe0b2,



        E_social_vimeo_circle = 0xe0b3,



        E_social_linkedin_circle = 0xe0b4,



        E_social_rss_circle = 0xe0b5,



        E_social_deviantart_circle = 0xe0b6,



        E_social_share_circle = 0xe0b7,



        E_social_myspace_circle = 0xe0b8,



        E_social_skype_circle = 0xe0b9,



        E_social_youtube_circle = 0xe0ba,



        E_social_picassa_circle = 0xe0bb,



        E_social_googledrive_alt2 = 0xe0bc,



        E_social_flickr_circle = 0xe0bd,



        E_social_blogger_circle = 0xe0be,



        E_social_spotify_circle = 0xe0bf,



        E_social_delicious_circle = 0xe0c0,



        E_social_facebook_square = 0xe0c1,



        E_social_twitter_square = 0xe0c2,



        E_social_pinterest_square = 0xe0c3,



        E_social_googleplus_square = 0xe0c4,



        E_social_tumblr_square = 0xe0c5,



        E_social_stumbleupon_square = 0xe0c6,



        E_social_wordpress_square = 0xe0c7,



        E_social_instagram_square = 0xe0c8,



        E_social_dribbble_square = 0xe0c9,



        E_social_vimeo_square = 0xe0ca,



        E_social_linkedin_square = 0xe0cb,



        E_social_rss_square = 0xe0cc,



        E_social_deviantart_square = 0xe0cd,



        E_social_share_square = 0xe0ce,



        E_social_myspace_square = 0xe0cf,



        E_social_skype_square = 0xe0d0,



        E_social_youtube_square = 0xe0d1,



        E_social_picassa_square = 0xe0d2,



        E_social_googledrive_square = 0xe0d3,



        E_social_flickr_square = 0xe0d4,



        E_social_blogger_square = 0xe0d5,



        E_social_spotify_square = 0xe0d6,



        E_social_delicious_square = 0xe0d7,



        E_icon_printer = 0xe103,



        E_icon_calulator = 0xe0ee,



        E_icon_building = 0xe0ef,



        E_icon_floppy = 0xe0e8,



        E_icon_drive = 0xe0ea,



        E_icon_search_2 = 0xe101,



        E_icon_id = 0xe107,



        E_icon_id_2 = 0xe108,



        E_icon_puzzle = 0xe102,



        E_icon_like = 0xe106,



        E_icon_dislike = 0xe0eb,



        E_icon_mug = 0xe105,



        E_icon_currency = 0xe0ed,



        E_icon_wallet = 0xe100,



        E_icon_pens = 0xe104,



        E_icon_easel = 0xe0e9,



        E_icon_flowchart = 0xe109,



        E_icon_datareport = 0xe0ec,



        E_icon_briefcase = 0xe0fe,



        E_icon_shield = 0xe0f6,



        E_icon_percent = 0xe0fb,



        E_icon_globe = 0xe0e2,



        E_icon_globe_2 = 0xe0e3,



        E_icon_target = 0xe0f5,



        E_icon_hourglass = 0xe0e1,



        E_icon_balance = 0xe0ff,



        E_icon_rook = 0xe0f8,



        E_icon_printer_alt = 0xe0fa,



        E_icon_calculator_alt = 0xe0e7,



        E_icon_building_alt = 0xe0fd,



        E_icon_floppy_alt = 0xe0e4,



        E_icon_drive_alt = 0xe0e5,



        E_icon_search_alt = 0xe0f7,



        E_icon_id_alt = 0xe0e0,



        E_icon_id_2_alt = 0xe0fc,



        E_icon_puzzle_alt = 0xe0f9,



        E_icon_like_alt = 0xe0dd,



        E_icon_dislike_alt = 0xe0f1,



        E_icon_mug_alt = 0xe0dc,



        E_icon_currency_alt = 0xe0f3,



        E_icon_wallet_alt = 0xe0d8,



        E_icon_pens_alt = 0xe0db,



        E_icon_easel_alt = 0xe0f0,



        E_icon_flowchart_alt = 0xe0df,



        E_icon_datareport_alt = 0xe0f2,



        E_icon_briefcase_alt = 0xe0f4,



        E_icon_shield_alt = 0xe0d9,



        E_icon_percent_alt = 0xe0da,



        E_icon_globe_alt = 0xe0de,



        E_icon_clipboard = 0xe0e6,
        #endregion
    }
}