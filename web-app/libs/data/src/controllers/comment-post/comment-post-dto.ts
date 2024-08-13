/** GET /api/v1/commentPost Query */
export type CommentPostGetListQuery = {
  postId: number;
};

/** GET /api/v1/commentPost Return */
export type CommentPostGetListReturn = {
  id: number;
  postId: number;
  userId: number;
  firstName: string;
  lastName: string;
  text: string;
}[];

/** POST /api/v1/commentPost Body */
export type CommentPostPostBody = {
  postId: number;
  userId: number;
  text: string;
};

/** PUT /api/v1/commentPost Body */
export type CommentPostPutBody = {
  id: number;
  text: string;
};
