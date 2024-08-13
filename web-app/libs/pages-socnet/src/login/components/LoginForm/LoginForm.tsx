import { SubmitHandler, useForm } from 'react-hook-form';
import { useRouter } from 'next/navigation';
import Link from 'next/link';
import { getAuthService, Input, ROUTES } from '@socnet/shared';
import { LoginFormFields } from './LoginForm.types';
import styles from './LoginForm.module.scss';

export const LoginForm = () => {
  const { register, handleSubmit } = useForm<LoginFormFields>();
  const router = useRouter();
  const login: SubmitHandler<LoginFormFields> = (data) =>
    getAuthService()
      .logIn(data)
      .then(() => router.push(ROUTES.PROFILE))
      .catch();

  return (
    <form className={styles.form} onSubmit={handleSubmit(login)}>
      <div className={styles.title}>С возвращением в SocialNet!</div>
      <Input
        className={styles.ic1}
        placeholder="Введите email..."
        register={register('email')}
      />
      <Input
        className={styles.ic2}
        placeholder="Введите пароль..."
        register={register('password')}
      />
      <button className={styles.submit} type="submit">
        Войти
      </button>
      <Link href="/registration">Вы здесь впервые?</Link>
      <Link href="/lol">Забыли пароль?</Link>
    </form>
  );
};
