import React from 'react';
import './Settings.css';
import SettingPrivacy from '../../widgets/SettingPrivacy/SettingPrivacy';
import SettingNotifications from '../../widgets/SettingNotifications/SettingNotifications';

const Settings = () => {
    return (
        <div className='settingNav'>
          <SettingPrivacy/>
          <SettingNotifications/>
        </div>
    );
}

export default Settings;