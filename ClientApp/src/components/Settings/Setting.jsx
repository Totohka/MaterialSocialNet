import React from 'react';
import './Settings.css';
import { NavLink } from 'react-router-dom';
import SettingPrivacyContainer from './SettingPrivacy/SettingPrivacyContainer';
import SettingNotificationsContainer from './SettingNotifications/SettingNotificationContainer';

const Settings = (props) => {
    return (
        <div className='settingNav'>
          {/* <div className='settingNavButton'>
            <NavLink to='/settingAccount'>Аккаунт и внешний вид</NavLink>
          </div>
          <div className='settingNavButton'>
            <NavLink to='/settingSecurity'>Безопасность</NavLink>
          </div> */}
          <SettingPrivacyContainer/>
          <SettingNotificationsContainer/>
          {/* <div className='settingNavButton'>
            <NavLink to='/settingPrivacy'>Приватность</NavLink>
          </div>
          <div className='settingNavButton'>
            <NavLink to='/settingNotification'>Уведомления</NavLink>
          </div> */}
          {/* <div className='settingNavButton'>
            <NavLink to='/settingBlacklist'>Чёрный список</NavLink>
          </div> */}
        </div>
    );
}

export default Settings;