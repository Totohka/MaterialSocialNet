/** GET /api/v1/post/{id} Return */
export type PostGetReturn = {
  id: number;
  /** Путь до фотографии */
  image: string | null;
  title: string | null;
  dateCreate: Date | string;
  userId: number;
  firstNameUser: string | null;
  lastNameUser: string | null;
  text: string | null;
  tags: string | null;
  rating: number;
  typeReaction: string | null;
};

/** DELETE /api/v1/post/{id} Query */
export type PostDeleteQuery = {
  userId: number;
};

/** GET /api/v1/post Query */
export type PostGetListQuery = {
  search: string;
  userId: number;
  number: number;
};

/** GET /api/v1/post Return */
export type PostGetListReturn = {
  countAllPosts: number;
  pageCount: number;
  numberPage: number;
  posts: PostGetReturn[] | null;
};

/** POST /api/v1/post Body */
export type PostPostBody = {
  image: File;
  title: string;
  dateCreate: Date;
  userId: number;
  text: string;
  tags: string;
};

/** PUT /api/v1/post Body */
export type PostPutBody = {
  id: number;
  image?: File;
  title: string;
  userId: number;
  text: string;
  tags: string;
};
