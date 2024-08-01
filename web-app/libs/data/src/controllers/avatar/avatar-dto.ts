/** POST /api/v1/avatar Body */
export type AvatarPostBody = {
  photo: File;
  userId: number;
};

/** PUT /api/v1/avatar Body */
export type AvatarPutBody = {
  photo: File;
  userId: number;
};
