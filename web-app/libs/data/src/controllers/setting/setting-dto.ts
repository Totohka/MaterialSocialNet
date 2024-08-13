/** PUT /api/v1/setting/privacy Body*/
export type SettingPrivacyPutBody = {
  userId: number;
  showPost: number;
  invateChats: number;
  showDateBirthday: number;
};

/** PUT /api/v1/setting/notification Body*/
export type SettingNotificationPutBody = {
  userId: number;
  newMessage: boolean;
  newSubscribe: boolean;
  newPosts: boolean;
};
