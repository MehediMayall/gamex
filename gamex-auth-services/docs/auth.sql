select ua.ip_address, browser, browser_version, browser_engine, os,  device, device_type, 
is_login_success_full, attempted_user_name, attempted_password, login_failed_reason, device_info, location


from public.user_log_activities ua
    LEFT JOIN public.users u ON ua.user_id = u.id
    LEFT JOIN public.players p ON u.player_id = p.id
    ORDER BY ua.created_on desc 