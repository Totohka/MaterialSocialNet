import React from "react";
import "./ProfileInfo.css";
import ProfileDescription from "../ProfileDescription/ProfileDescription";
import ProfileStatus from "../ProfileStatus/ProfileStatus";
import ProfileAvatar from "../ProfileAvatar/ProfileAvatar";
import ProfileBackground from "../ProfileBackground/ProfileBackground";



const ProfileInfo = () => {

  return (
    <div>
      <div className="profile_info">
        <ProfileBackground/>
        <ProfileAvatar/>
        <ProfileDescription/>
      </div>
      <ProfileStatus/>
    </div>
  );
};

export default ProfileInfo;
