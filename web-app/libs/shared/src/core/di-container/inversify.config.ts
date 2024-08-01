import { Container } from 'inversify';
import { TYPES } from './types';
import { EnvironmentService } from '../environment-service';
import { AuthService } from '../auth';
import { HttpBackendClient } from '../http';
import {
  AvatarRepository,
  BackgroundRepository,
  ChatRepository,
  CommentPostRepository,
  GalleryRepository,
  InvateRepository,
  MessageRepository,
  PostRepository,
  ReactionMessageRepository,
  ReactionPostRepository,
  SettingRepository,
  SubscribeRepository,
  UserRepository,
} from '@socnet/data';
import 'reflect-metadata';

const DIContainer = new Container();
DIContainer.bind<EnvironmentService>(TYPES.EnvReader).to(EnvironmentService);
DIContainer.bind<AuthService>(TYPES.AuthClient).to(AuthService);
DIContainer.bind<HttpBackendClient>(TYPES.HttpBackendClient).to(
  HttpBackendClient
);
DIContainer.bind<UserRepository>(TYPES.UserRepository).to(UserRepository);
DIContainer.bind<ChatRepository>(TYPES.ChatRepository).to(ChatRepository);
DIContainer.bind<InvateRepository>(TYPES.InvateRepository).to(InvateRepository);
DIContainer.bind<MessageRepository>(TYPES.MessageRepository).to(
  MessageRepository
);
DIContainer.bind<PostRepository>(TYPES.PostRepository).to(PostRepository);
DIContainer.bind<CommentPostRepository>(TYPES.PostRepository).to(
  CommentPostRepository
);
DIContainer.bind<ReactionPostRepository>(TYPES.ReactionPostRepository).to(
  ReactionPostRepository
);
DIContainer.bind<ReactionMessageRepository>(TYPES.ReactionMessageRepository).to(
  ReactionMessageRepository
);
DIContainer.bind<SubscribeRepository>(TYPES.SubscribeRepository).to(
  SubscribeRepository
);
DIContainer.bind<SettingRepository>(TYPES.SettingRepository).to(
  SettingRepository
);
DIContainer.bind<AvatarRepository>(TYPES.AvatarRepository).to(AvatarRepository);
DIContainer.bind<BackgroundRepository>(TYPES.BackgroundRepository).to(
  BackgroundRepository
);
DIContainer.bind<GalleryRepository>(TYPES.GalleryRepository).to(
  GalleryRepository
);

export { DIContainer };
