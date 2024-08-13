/** POST /api/v1/subscribe Body*/
export type SubscribePostBody = {
  userId: number;
  userFriendId: number;
};

/** DELETE /api/v1/subscribe Body*/
export type SubscribeDeleteBody = {
  userId: number;
  userFriendId: number;
};
