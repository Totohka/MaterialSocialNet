/** GET /api/v1/gallery Query */
export type GalleryGetQuery = {
  userId: number;
  take: number;
};

/** GET /api/v1/gallery Return */
export type GalleryGetReturn = string[];

/** POST /api/v1/gallery Body */
export type GalleryPostBody = {
  photo: File;
  userId: number;
};

/** PUT /api/v1/gallery Body */
export type GalleryPutBody = {
  photo: File;
  userId: number;
  photoId: number;
};

/** DELETE /api/v1/gallery/{imageId} Query */
export type GalleryDeleteQuery = {
  userId: number;
};
