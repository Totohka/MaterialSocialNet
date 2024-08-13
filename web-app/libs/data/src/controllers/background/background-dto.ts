/** POST /api/v1/background Body */
export type BackgroundPostBody = {
  photo: File;
  userId: number;
};

/** PUT /api/v1/background Body */
export type BackgroundPutBody = {
  photo: File;
  userId: number;
};
